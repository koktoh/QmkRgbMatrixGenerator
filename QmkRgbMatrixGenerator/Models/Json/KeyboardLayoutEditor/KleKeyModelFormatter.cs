using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    public class KleKeyModelFormatter : IJsonFormatter<KleKeyModel>
    {
        public KleKeyModel Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            KleOptionModel option = null;

            if (reader.ReadIsBeginObject())
            {
                reader.AdvanceOffset(-1);
                option = formatterResolver.GetFormatterWithVerify<KleOptionModel>().Deserialize(ref reader, formatterResolver);
            }

            if (!reader.ReadIsValueSeparator())
            {
                reader.AdvanceOffset(-1);
            }

            var label = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, formatterResolver);

            return new KleKeyModel(label, option);
        }

        public void Serialize(ref JsonWriter writer, KleKeyModel value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (value.Option != null)
            {
                formatterResolver.GetFormatterWithVerify<KleOptionModel>().Serialize(ref writer, value.Option, formatterResolver);
                writer.WriteValueSeparator();
            }

            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Legend, formatterResolver);
        }
    }
}
