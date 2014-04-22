/* Empiria Land 2014 ******************************************************************************************
*                                                                                                             *
*  Solution  : Empiria Land                                    System   : Land Registration System            *
*  Namespace : Empiria.Land.UI                                 Assembly : Empiria.Land                        *
*  Type      : LRSHtmlSelectControls                           Pattern  : Presentation Services Static Class  *
*  Version   : 5.5        Date: 25/Jun/2014                    License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                             *
*  Summary   : Static class that generates predefined HtmlSelect controls content for Empiria Government      *
*              Land Registration System.                                                                      *
*                                                                                                             *
********************************** Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace Empiria.Presentation.Web.Content {

  /// <summary>Static class that generates predefined HtmlSelect controls content for Empiria Government Land
  /// Registration System.</summary>
  static public class HtmlSelectContent {

    #region Public methods

    static public void AppendToCombo(HtmlSelect comboControl, IEnumerable dataSource, string dataValueField, string dataTextField) {
      IEnumerator enumerator = dataSource.GetEnumerator();
      if (enumerator.MoveNext()) {
        Object current = enumerator.Current;
        PropertyInfo valueProperty = current.GetType().GetProperty(dataValueField);
        PropertyInfo textProperty = current.GetType().GetProperty(dataTextField);

        comboControl.Items.Add(new ListItem((string) textProperty.GetValue(current, null),
                                            valueProperty.GetValue(current, null).ToString()));
        while (enumerator.MoveNext()) {
          current = enumerator.Current;
          comboControl.Items.Add(new ListItem((string) textProperty.GetValue(current, null),
                                              valueProperty.GetValue(current, null).ToString()));
        }
      }
    }

    static public void AppendToCombo<T>(HtmlSelect comboControl, IEnumerable dataSource,
                                        Func<T, string> dataValueFunction, Func<T, string> dataTextFunction) where T : IStorable {
      IEnumerator enumerator = dataSource.GetEnumerator();
      if (enumerator.MoveNext()) {
        T current = (T) enumerator.Current;
        comboControl.Items.Add(new ListItem(dataTextFunction.Invoke(current), dataValueFunction.Invoke(current)));
        while (enumerator.MoveNext()) {
          current = (T) enumerator.Current;
          comboControl.Items.Add(new ListItem(dataTextFunction.Invoke(current), dataValueFunction.Invoke(current)));
        }
      }
    }

    static public string GetComboAjaxHtml(IEnumerable dataSource, int maxCount, string dataValueField, string dataTextField,
                                          string headerItemText) {
      return GetComboAjaxHtml(dataSource, maxCount, dataValueField, dataTextField, headerItemText, String.Empty, String.Empty);
    }

    static public string GetComboAjaxHtml(IEnumerable dataSource, int maxCount, string dataValueField, string dataTextField,
                                          string headerItemText, string emptyItemText, string unknownItemText) {
      string html = GetComboAjaxHtml(headerItemText, emptyItemText, unknownItemText);

      if (html.Length != 0) {
        html += "|";
      }

      IEnumerator enumerator = dataSource.GetEnumerator();
      if (enumerator.MoveNext()) {
        int counter = 0;
        if (maxCount <= 0) {
          maxCount = Int32.MaxValue;
        }
        Object current = enumerator.Current;
        PropertyInfo valueProperty = current.GetType().GetProperty(dataValueField);
        PropertyInfo textProperty = current.GetType().GetProperty(dataTextField);

        html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                 (string) textProperty.GetValue(current, null)) + "|";
        counter++;
        while (enumerator.MoveNext() && counter < maxCount) {
          current = enumerator.Current;
          html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                   (string) textProperty.GetValue(current, null)) + "|";
          counter++;
        }
      }
      return html.TrimEnd('|');
    }

    static public string GetComboAjaxHtml<T>(ObjectList<T> dataSource, string dataValueField, Func<T, string> dataTextFunction,
                                             string headerItemText) where T : IStorable {
      string html = String.Empty;
      if (headerItemText.Length != 0) {
        html += GetComboAjaxHtmlItem(String.Empty, headerItemText);
      }
      if (html.Length != 0) {
        html += "|";
      }
      IEnumerator<T> enumerator = dataSource.GetEnumerator();

      if (enumerator.MoveNext()) {
        T current = enumerator.Current;
        PropertyInfo valueProperty = current.GetType().GetProperty(dataValueField);

        html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                 dataTextFunction.Invoke(current)) + "|";
        while (enumerator.MoveNext()) {
          current = enumerator.Current;
          html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                   dataTextFunction.Invoke(current)) + "|";
        }
      }
      return html.TrimEnd('|');
    }

    static public string GetComboAjaxHtml<T>(ObjectList<T> dataSource, string dataValueField, Func<T, string> dataTextFunction,
                                             string headerItemText, string emptyItemText, string unknownItemText) where T : IStorable {
      string html = GetComboAjaxHtml(headerItemText, emptyItemText, unknownItemText);

      if (html.Length != 0) {
        html += "|";
      }
      IEnumerator<T> enumerator = dataSource.GetEnumerator();

      if (enumerator.MoveNext()) {
        T current = enumerator.Current;
        PropertyInfo valueProperty = current.GetType().GetProperty(dataValueField);

        html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                 dataTextFunction.Invoke(current)) + "|";
        while (enumerator.MoveNext()) {
          current = enumerator.Current;
          html += GetComboAjaxHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                   dataTextFunction.Invoke(current)) + "|";
        }
      }
      return html.TrimEnd('|');
    }

    static public string GetComboAjaxHtml(string headerItemText, string emptyItemText, string unknownItemText) {
      string html = String.Empty;

      if (headerItemText.Length != 0) {
        html += GetComboAjaxHtmlItem(String.Empty, headerItemText) + "|";
      }
      if (emptyItemText.Length != 0) {
        html += GetComboAjaxHtmlItem("-1", emptyItemText) + "|";
      }
      if (unknownItemText.Length != 0) {
        html += GetComboAjaxHtmlItem("-2", unknownItemText);
      }
      return html.TrimEnd('|');
    }

    static public string GetComboAjaxHtmlItem(string comboItemValue, string comboItemText) {
      return comboItemValue + "~" + comboItemText;
    }

    static public string GetComboHtml(IEnumerable dataSource, string dataValueField, string dataTextField,
                                      string headerItemText) {
      return GetComboHtml(dataSource, dataValueField, dataTextField, headerItemText, String.Empty, String.Empty);
    }

    static public string GetComboHtml(IEnumerable dataSource, string dataValueField, string dataTextField,
                                      string headerItemText, string emptyItemText, string unknownItemText) {
      string html = GetComboHtml(headerItemText, emptyItemText, unknownItemText);

      IEnumerator enumerator = dataSource.GetEnumerator();
      if (enumerator.MoveNext()) {
        Object current = enumerator.Current;
        PropertyInfo valueProperty = current.GetType().GetProperty(dataValueField);
        PropertyInfo textProperty = current.GetType().GetProperty(dataTextField);

        html += GetComboHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                 (string) textProperty.GetValue(current, null));
        while (enumerator.MoveNext()) {
          current = enumerator.Current;
          html += GetComboHtmlItem(valueProperty.GetValue(current, null).ToString(),
                                  (string) textProperty.GetValue(current, null));
        }
      }
      return html;
    }

    static public string GetComboHtml(string headerItemText, string emptyItemText, string unknownItemText) {
      string html = String.Empty;

      if (headerItemText.Length != 0) {
        html += GetComboHtmlItem(String.Empty, headerItemText);
      }
      if (emptyItemText.Length != 0) {
        html += GetComboHtmlItem("-1", emptyItemText);
      }
      if (unknownItemText.Length != 0) {
        html += GetComboHtmlItem("-2", unknownItemText);
      }
      return html;
    }

    static public string GetComboHtmlItem(string comboItemValue, string comboItemText) {
      return "<option value='" + comboItemValue + "'>" + comboItemText + "</option>";
    }

    static public string GetSearchResultHeaderText(Ontology.ObjectTypeInfo typeInfo, int count) {
      if (count == 0) {
        return "(No se encontraron " + typeInfo.DisplayPluralName.ToLowerInvariant() +
               " con la información proporcionada)";
      } else if (count == 1) {
        return (typeInfo.FemaleGenre ? "( Una " : "( Un ") +
               typeInfo.DisplayName.ToLowerInvariant() + (typeInfo.FemaleGenre ? " encontrada )" : " encontrado )");
      } else {
        return "( " + EmpiriaString.SpeechInteger(count) + " " +
               typeInfo.DisplayPluralName.ToLowerInvariant() +
              (typeInfo.FemaleGenre ? " encontradas )" : " encontrados )");
      }
    }

    static public void LoadCombo(HtmlSelect comboControl, IEnumerable dataSource, string dataValueField, string dataTextField) {
      LoadCombo(comboControl, dataSource, dataValueField, dataTextField, String.Empty, String.Empty, String.Empty);
    }

    static public void LoadCombo(HtmlSelect comboControl, IEnumerable dataSource, string dataValueField, string dataTextField,
                                 string headerItemText) {
      LoadCombo(comboControl, dataSource, dataValueField, dataTextField, headerItemText, String.Empty, String.Empty);
    }

    static public void LoadCombo(HtmlSelect comboControl, IEnumerable dataSource, string dataValueField, string dataTextField,
                                 string headerItemText, string emptyItemText, string unknownItemText) {
      comboControl.DataSource = dataSource;
      comboControl.DataValueField = dataValueField;
      comboControl.DataTextField = dataTextField;
      comboControl.DataBind();

      LoadCombo(comboControl, headerItemText, emptyItemText, unknownItemText);
    }

    static public void LoadCombo<T>(HtmlSelect comboControl, IEnumerable dataSource, Func<T, string> dataValueFunction, Func<T, string> dataTextFunction,
                                    string headerItemText) where T : IStorable {
      comboControl.Items.Clear();
      AppendToCombo(comboControl, dataSource, dataValueFunction, dataTextFunction);
      LoadCombo(comboControl, headerItemText, String.Empty, String.Empty);
    }

    static public void LoadCombo(HtmlSelect comboControl, string headerItemText, string emptyItemText,
                                 string unknownItemText) {
      if (unknownItemText.Length != 0) {
        comboControl.Items.Insert(0, new ListItem(unknownItemText, "-2"));
      }
      if (emptyItemText.Length != 0) {
        comboControl.Items.Insert(0, new ListItem(emptyItemText, "-1"));
      }
      if (headerItemText.Length != 0) {
        comboControl.Items.Insert(0, new ListItem(headerItemText, String.Empty));
      }
    }

    #endregion Public methods

  } // class HtmlSelectContent

} // namespace Empiria.Presentation.Web.Content