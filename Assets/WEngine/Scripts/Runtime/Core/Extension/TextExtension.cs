﻿using UnityEngine;
using UnityEngine.UI;

namespace WEngine
{
    public static class TextExtension
    {
        public static void SetTextWithEllipsis(this Text textComponent, string value)
        {
            // create generator with value and current Rect
            var generator = new TextGenerator();
            var rectTransform = textComponent.GetComponent<RectTransform>();
            var settings = textComponent.GetGenerationSettings(rectTransform.rect.size);
            //generator.Populate(value, settings);
            generator.PopulateWithErrors(value, settings, textComponent.gameObject);

            // trncate visible value and add ellipsis
            var characterCountVisible = generator.characterCountVisible;
            var updatedText = value;
            if (value.Length > characterCountVisible)
            {
                updatedText = value.Substring(0, characterCountVisible - 1);
                updatedText += "…";
            }

            // update text
            textComponent.text = updatedText;
        }
    }
}