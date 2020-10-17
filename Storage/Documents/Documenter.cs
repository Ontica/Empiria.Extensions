/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : Documenter                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to fill out the defined documentation of an entity in a given context.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Storage.Documents {

  /// <summary>Provides services to fill out the defined documentation
  /// of an entity in a given context.</summary>
  public class Documenter {

    private readonly IIdentifiable _definer;
    private readonly IIdentifiable _target;

    public Documenter(IIdentifiable definer, IIdentifiable target) {
      _definer = definer;
      _target = target;
      this.Documentation = new Documentation();
    }

    public Documentation Documentation {
      get;
      internal set;
    }

  }  // class Documenter

}  // namespace Empiria.Storage.Documents
