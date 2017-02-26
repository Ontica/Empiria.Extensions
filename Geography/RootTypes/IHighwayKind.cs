/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : IHighwayKind                                   Pattern  : Loose coupling interface            *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Interface that represents a highway type kind: federal, state, municipal and rural.           *
*                                                                                                            *
********************************* Copyright (c) 2009-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Geography {

  /// <summary>Interface that represents a highway type kind: federal, state, municipal and rural.</summary>
  public interface IHighwayKind : IValueObject<string> {

  } // interface IHighwayKind

}  // namespace Empiria.Geography
