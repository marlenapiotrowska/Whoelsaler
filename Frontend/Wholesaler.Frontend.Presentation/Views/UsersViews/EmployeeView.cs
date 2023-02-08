﻿using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.EmployeeViews;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class EmployeeView : View
    {
        private readonly StartWorkdayView _startWorkday;
        private readonly FinishWorkdayView _finishWorkday;
        private readonly ReviewAssignedTasksView _reviewAssignedTasks;

        public EmployeeView(StartWorkdayView startWorkday,  ApplicationState state, FinishWorkdayView finishWorkday, ReviewAssignedTasksView reviewAssignedTasks)
            : base(state)
        {
            _startWorkday = startWorkday;
            _finishWorkday = finishWorkday;
            _reviewAssignedTasks = reviewAssignedTasks;
        }

        protected override async Task RenderViewAsync()
        {
            var wasExitKeyPressed = false;

            while (wasExitKeyPressed == false)
            {
                Console.WriteLine("---Welcome in Wholesaler---");
                Console.WriteLine
                    ("\n[1] To record ending of the row" +
                    "\n[2] To start work" +
                    "\n[3] To finish work" +
                    "\n[4] To get information about assigned tasks" +
                    "\n[ESC] To quit");

                var pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:                        
                        await _startWorkday.RenderAsync();                       
                        continue;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        await _finishWorkday.RenderAsync();
                        continue;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        await _reviewAssignedTasks.RenderAsync();
                        continue;

                    case ConsoleKey.Escape:
                        wasExitKeyPressed = true;
                        break;

                    default: continue;
                }
            }
        }
    }
}
