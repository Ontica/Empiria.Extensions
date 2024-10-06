/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Use cases Layer                         *
*  Assembly : Empiria.Artifacts.dll                      Pattern   : Use case interactor class               *
*  Type     : SoftwareProductUseCases                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for read and write software products or systems.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

namespace Empiria.Artifacts.UseCases {

  /// <summary>Use cases for read and write software features.</summary>
  public class SoftwareProductUseCases : UseCase {

    #region Constructors and parsers

    protected SoftwareProductUseCases() {
      // no-op
    }

    static public SoftwareProductUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<SoftwareProductUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> GetSoftwareProducts() {
      return SoftwareProduct.GetList()
                            .MapToNamedEntityList();
    }

    #endregion Use cases

  } // class SoftwareFeaturesUseCases

} // namespace Empiria.Artifacts.UseCases
