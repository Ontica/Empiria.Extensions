/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Aspects                            Component : Infrastructure provider                 *
*  Assembly : Empiria.Aspects.dll                        Pattern   : Assembly Attributes File                *
*  Type     : None                                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides an infrastructure for handling the execution and modularization of Aspect-oriented    *
*             programming (AOP) cross-cutting concerns like security, logging, exception handling, or        *
*             transaction management.                                                                        *
*                                                                                                            *
*             Also allows the enforcing of Design by Contract (DbC) principles: Preconditions,               *
*             postconditions and class invariants.                                                           *
*                                                                                                            *
*             In general, provides types that changes or adds new behavior to existing code in runtime,      *
*             executing system's or business's policies and rules that can be defined and configured in      *
*             data and linked using dynamic binding.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;

/*************************************************************************************************************
* Assembly configuration attributes.                                                                         *
*************************************************************************************************************/
[assembly: AssemblyTrademark("Empiria and Ontica are either trademarks of La Vía Óntica SC or Ontica LLC.")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
