﻿using System.Reflection;
using ProcessorSim.Instructions;
using ProcessorSim.Variables;

namespace ProcessorSim
{
    class Program
    {
        static void Main()
        {
            var processor = new Processor(100, 7);
            var instructionExecutor = new InstructionExecutor(processor);
            var actionRetriever = new InstructionRetriever(instructionExecutor);
            var instructionsResolver = new InstructionsResolver(processor, actionRetriever);
            var variablesRetriever = new VariablesRetriever();

            var variables = variablesRetriever.GetVariables(processor.InstructionsToExecute);

            foreach (var variable in variables)
            {
                processor.LoadValIntoStack(instructionExecutor, variable);
            }

            processor.SetVariables(variables);

            while (processor.CurrentInstructionNum < processor.InstructionsToExecute.Count)
            {
                if (processor.ShouldStop)
                {
                    break;
                }

                var instruction = processor.GetInstruction();
                processor.ExecuteInstruction(instructionsResolver, instruction);
                processor.CurrentInstructionNum++;
            }
        }
    }
}
