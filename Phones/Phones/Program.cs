using System;
using System.IO;
using System.Linq;

namespace Phones
{
    class Program
    {
        private static readonly string Template =
            @"BEGIN:VCARD" + Environment.NewLine +
            "VERSION:3.0" + Environment.NewLine +
            "FN:{0}" + Environment.NewLine +
            "N:;{0};;;" + Environment.NewLine +
            "TEL;TYPE=CELL:{1}" + Environment.NewLine +
            "END:VCARD";

        static void Main(string[] args)
        {
            if (args.Length != 2 || !File.Exists(args[0]))
            {
                Console.WriteLine("Invalid arguments");
                Console.WriteLine("Correct usage: Phones.exe source-file output-file");
                return;
            }

            var contacts = File.ReadAllLines(args[0]);
            var newContacts = contacts.Select(Transform);
            File.WriteAllLines(args[1], newContacts);
        }

        private static string Transform(string contact)
        {
            var split = contact.Split(',');
            var name = split[0];
            var number = split[1];
            return string.Format(Template, name, number);
        }
    }
}
