using System;
using System.Collections.Generic;
using System.Linq;
using QmkRgbMatrixGenerator.Extensions;
using QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor;
using QmkRgbMatrixGenerator.Models.RgbMatrix;

namespace QmkRgbMatrixGenerator.Models.Converter
{
    public class KleConverter
    {
        public RgbLightCollection ConvertToRgbLights(KleLayoutModel kle)
        {
            var rgbLights = this.ConvertToRgbMatrix(kle);
            var normalized = this.NormalizeRgbLightPos(rgbLights);
            var ordered = normalized.OrderBy(x => x.Index);

            return new RgbLightCollection(ordered);
        }

        private IEnumerable<RgbLightModel> NormalizeRgbLightPos(IEnumerable<RgbLightModel> rgbLights)
        {
            double constX = 224;
            double constY = 64;

            var maxWidth = rgbLights.Max(light => light.X);
            var maxHeight = rgbLights.Max(light => light.Y);

            var unitX = constX / maxWidth;
            var unitY = constY / maxHeight;

            return rgbLights.Select(light =>
            {
                var x = light.X * unitX;
                var y = light.Y * unitY;

                if (x > constX)
                {
                    x = constX;
                }

                if (y > constY)
                {
                    y = constY;
                }

                light.X = (int)x;
                light.Y = (int)y;

                return light;
            });
        }

        private IEnumerable<RgbLightModel> ConvertToRgbMatrix(KleLayoutModel kle)
        {
            double offsetX = 0;
            double preWidth = 1;

            for (int rowIndex = 0; rowIndex < kle.KleRows.Count(); rowIndex++)
            {
                offsetX = 0;
                preWidth = 1;
                var kleRow = kle.KleRows.ElementAt(rowIndex);

                for (int colIndex = 0; colIndex < kleRow.KleKeys.Count(); colIndex++)
                {
                    var kleKey = kleRow.KleKeys.ElementAt(colIndex);

                    var width = kleKey.Option?.Width ?? 1;
                    var height = kleKey.Option?.Height ?? 1;
                    offsetX += (kleKey.Option?.OffsetX ?? 0) + preWidth - 1;
                    preWidth = width;

                    var posX = this.CalculatePosX(colIndex, offsetX, kleKey);
                    var posY = this.CalculatePosY(rowIndex, kleKey);

                    yield return new RgbLightModel
                    {
                        Id = kleKey.Legend.Split("\n").Last(x => x.HasMeaningfulValue()),
                        Index = Convert.ToInt32(kleKey.LegendTopLeft),
                        X = posX,
                        Y = posY,
                        Flag = kleKey.Option?.IsDecal ?? false ? RgbLightFlag.LED_FLAG_UNDERGLOW : RgbLightFlag.LED_FLAG_KEYLIGHT,
                    };
                }
            }
        }

        private double CalculatePosX(double colIndex, double offset, KleKeyModel kleKey)
        {
            var width = kleKey.Option?.Width ?? 1;

            return this.CalculatePos(colIndex, offset, width);
        }

        private double CalculatePosY(double rowIndex, KleKeyModel kleKey)
        {
            var height = kleKey.Option?.Height ?? 1;

            return this.CalculatePos(rowIndex, 0, height);
        }

        private double CalculatePos(double index, double offset, double length)
        {
            var center = (length - 1) / 2;

            if (length == 1)
            {
                center = 0;
            }

            return index + offset + center;
        }
    }
}
