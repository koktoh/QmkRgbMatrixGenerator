using System.Collections.Generic;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    public class KleRowModelFormatter : IJsonFormatter<KleRowModel>
    {
        public KleRowModel Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            var formatter = formatterResolver.GetFormatter<KleKeyModel>();
            var list = new List<KleKeyModel>();

            var count = 0;

            while (reader.ReadIsInArray(ref count))
            {
                var key = formatter.Deserialize(ref reader, formatterResolver);

                list.Add(key);
            }

            return new KleRowModel(list);
        }

        public void Serialize(ref JsonWriter writer, KleRowModel value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            formatterResolver.GetFormatter<IEnumerable<KleKeyModel>>().Serialize(ref writer, value.KleKeys, formatterResolver);
        }
    }
}
