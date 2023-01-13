using GarageDataAccess.DataAccess;
using GarageDataAccess.Models;

while (true)
{



    /// Skapar en meny och låter användaren välja olika alternativ

    Console.WriteLine("-------- Detta är ditt garage --------");
    Console.WriteLine("-------- Gör något av följande alternativ --------");
    Console.WriteLine("1. Lägg till en ny bil");
    Console.WriteLine("2. Kontrollera vilka bilar som finns");
    Console.WriteLine("3. Uppdatera garaget");
    Console.WriteLine("4. Ta bort en bil");
    Console.WriteLine("5. Exit");





    MongoDataAccess db = new MongoDataAccess();   //Skapar ett objekt av MongoDataAccess

    //await db.CreateCar(new CarModel()   //Ger förutbestämda värden till objekten
    //{
    //    Brand = "Volvo", Colour = "Gul"
    //});

    //await db.CreateCar(new CarModel()
    //{
    //    Brand = "Ferrari", Colour = "Röd"
    //});

    //await db.CreateCar(new CarModel()
    //{
    //    Brand = "Nissan", Colour = "Vit"
    //});

    //await db.CreateCar(new CarModel()
    //{
    //    Brand = "Skoda", Colour = "Blå"
    //});


    int customerChoice = db.InputControl();   //Hänvisar till metoden i MongoDataAccess

    int counter = 1;

    switch (customerChoice)   //En switch-sats som låter användaren att se befintliga bilar, lägga till, uppdatera, och radera dem. Ge värde åt CRUD:en.
    {
        case 1:
            Console.WriteLine("Ange bilmärke");
            string brand = Console.ReadLine();
            Console.WriteLine("Ange färg");
            string colour = Console.ReadLine();



            var CarModel = new CarModel { Brand = brand, Colour = colour };
            await db.CreateCar(CarModel);    //Insert  




            break;



        case 2:
            var carCollection = await db.GetAllCars();   //Show


            foreach (var cars in carCollection)
            {
                Console.WriteLine(cars.Brand);
            }

            break;



        case 3:
            var carCollection2 = await db.GetAllCars();
            counter = 1;


            foreach (var cars in carCollection2)
            {
                Console.WriteLine(counter + " . " + cars.Brand);
                counter++;
            }

            Console.WriteLine("Vilken av bilarna vill du uppdatera, välj en siffra");
            int val = int.Parse(Console.ReadLine());

            Console.WriteLine("Ange bilmärke");
            string brand2 = Console.ReadLine();
            Console.WriteLine("Ange färg");
            string colour2 = Console.ReadLine();

            carCollection2[val - 1].Brand = brand2;
            carCollection2[val - 1].Colour = colour2;

            await db.UpdateCar(carCollection2[val - 1]);   //Update



            break;


        case 4:
            var carCollection3 = await db.GetAllCars();
            counter = 1;


            foreach (var cars in carCollection3)
            {
                Console.WriteLine(counter + " . " + cars.Brand);
                counter++;
            }

            Console.WriteLine("Vilken av bilarna vill du ta bort, välj en siffra");
            int val2 = int.Parse(Console.ReadLine());


            await db.DeleteCar(carCollection3[val2 - 1]);   //Delete



            break;

        case 5:

            Environment.Exit(1);  //Exit

            break;


    }

}
