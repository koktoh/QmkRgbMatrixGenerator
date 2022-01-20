namespace QmkRgbMatrixGenerator.Models.RgbMatrix
{
    public enum RgbLightFlag
    {
        LED_FLAG_NONE = 0x00,
        LED_FLAG_ALL = 0xFF,
        LED_FLAG_MODIFIER = 0x01,
        LED_FLAG_UNDERGLOW = 0x02,
        LED_FLAG_KEYLIGHT = 0x04,
        LED_FLAG_INDICATOR = 0x08,
    }
}
