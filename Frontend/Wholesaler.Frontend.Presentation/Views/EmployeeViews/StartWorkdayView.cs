﻿using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class StartWorkdayView : View
    {
        private readonly IUserService _service;
        private readonly StartWorkdayState _state;

        public StartWorkdayView(IUserService service, ApplicationState state)
            : base(state)
        {
            _service = service;
            _state = state.GetEmployeeViews().GetStartWorkday();
            _state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            var id = State.GetLoggedInUser().Id;
            var startWorking = await _service.StartWorkingAsync(id);

            if (startWorking.IsSuccess)
                _state.StartWork(startWorking.Payload);

            else
                throw new InvalidDataProvidedException(startWorking.Message);

            var workdayId = _state.GetStartedWorkdayId();

            var newWorkday = await _service.GetWorkdayAsync(workdayId);

            if (newWorkday.IsSuccess)
                _state.StartWorkday(newWorkday.Payload);

            else
                throw new InvalidDataProvidedException(newWorkday.Message);

            Console.WriteLine($"You started your work at: {_state.GetWorkday().Start}");
            Console.ReadLine();
        }
    }
}
