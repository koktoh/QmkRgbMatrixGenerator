using System.Collections.Generic;
using Utf8Json;

namespace QmkRgbMatrixGenerator.Models.Json.KeyboardLayoutEditor
{
    public class KleLayoutModelFormatter : IJsonFormatter<KleLayoutModel>
    {
        public KleLayoutModel Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            var formatter = formatterResolver.GetFormatter<KleRowModel>();
            var list = new List<KleRowModel>();

            var count = 0;

            while (reader.ReadIsInArray(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return new KleLayoutModel(list);
        }

        public void Serialize(ref JsonWriter writer, KleLayoutModel value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            formatterResolver.GetFormatter<IEnumerable<KleRowModel>>().Serialize(ref writer, value.KleRows, formatterResolver);
        }
    }
}
