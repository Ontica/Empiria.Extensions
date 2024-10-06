/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Test cases                              *
*  Assembly : Empiria.Artifacts.Tests.dll                Pattern   : Use cases tests                         *
*  Type     : SoftwareProductUseCasesTests               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use case tests for software products.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Artifacts.UseCases;

namespace Empiria.Tests.Artifacts {

  /// <summary>Use case tests for software products.</summary>
  public class SoftwareProductUseCasesTests {

    #region Facts

    [Fact]
    public void Should_Get_Software_Products() {

      using (var usecase = SoftwareProductUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> products = usecase.GetSoftwareProducts();

        Assert.NotEmpty(products);
      }
    }

    #endregion Facts

  }  // class SoftwareFeaturesUseCases

}  // namespace Empiria.Tests.Artifacts
