using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjekategorijaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.Sql(@"INSERT INTO Saloni (Naziv, Adresa, KontaktTelefon, Email, ProsjecnaOcjena, Opis, RadnoVrijemeOd, RadnoVrijemeDo, SubotaRadna, NaslovnaFotografija)
                           VALUES
                           ('Beauty Salon Glam', 'Ul. Marka Marulica 15', '+387 33 123 456', 'info@beautyglam.ba', 0, 'Naš moderno opremljen frizerski salon nudi vrhunske usluge njege kose, uključujući šišanje, farbanje, oblikovanje i tretmane za revitalizaciju kose. Također, pružamo i profesionalne kozmetičke usluge, kako bismo osigurali sveobuhvatan doživljaj njege i ljepote za naše klijente. S modernim pristupom, stručnošću i najnovijim proizvodima, naš cilj je pružiti uslugu koja zadovoljava sve vaše potrebe za ljepotom i njegom.', '09:00:00', '20:00:00', 1, 'https://images.pexels.com/photos/16935956/pexels-photo-16935956/free-photo-of-woman-doing-her-hair-on-a-backstage.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'),
                           ('Nails by Belma', 'Ul. Kulina Bana 8', '+387 33 654 321', 'info@nailsbybelma.ba', 0, 'Naš salon specijaliziran za njegu noktiju nudi profesionalne usluge koje garantiraju savršen izgled vaših noktiju. S našim stručnim osobljem, koje koristi samo visokokvalitetne proizvode, pružamo precizne i detaljne usluge manikira i pedikira, prilagođene vašim potrebama. Posvećeni smo tome da svaki klijent napusti salon s osjećajem zadovoljstva i ljepote, jer nam je cilj postići besprijekoran rezultat i dugotrajan efekt..', '10:00:00', '18:00:00', 1, 'https://images.pexels.com/photos/7755658/pexels-photo-7755658.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'),
                           ('Salon Elegancija', 'Sjeverni logor 23', '+387 61 234 567', 'elegancija@gmail.com', 0, 'Ekskluzivni salon za frizure i njegu kose nudi luksuznu uslugu koja zadovoljava najviše standarde. S našim timom vrhunskih stručnjaka i korištenjem najkvalitetnijih proizvoda, pružamo personalizirane tretmane za kosu, uključujući šišanje, farbanje, oblikovanje i specijalizirane tretmane za revitalizaciju kose. Naš cilj je pružiti klijentima jedinstveno iskustvo, osiguravajući da se osjećaju posebno i da izlaze iz salona s savršenim izgledom i zdravom kosom.', '09:00:00', '20:00:00', 1, 'https://images.pexels.com/photos/7750104/pexels-photo-7750104.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'),
                           ('Zen Beauty Studio', 'Ul. Marsala Tita 30', '+387 65 444 888', 'zenbeauty@gmail.com', 0, 'Miran prostor za opuštanje i njegu, specijaliziran za tretmane lica koji osiguravaju potpuno zadovoljstvo i revitalizaciju. S fokusom na kvalitet i pažnju prema detaljima, pružamo personalizirane usluge koje uključuju čišćenje, hidrataciju, anti-age tretmane i sve ostale usluge koje će vašu kožu učiniti svježom i blistavom. U opuštajućem ambijentu, naši stručnjaci koriste visokokvalitetne proizvode kako bi osigurali najbolje rezultate i pružili vam osjećaj potpuno njege i relaksacije.', '11:00:00', '18:00:00', 1, 'https://images.pexels.com/photos/27165064/pexels-photo-27165064/free-photo-of-counter-in-luxury-house.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1')");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
