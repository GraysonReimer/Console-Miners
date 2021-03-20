using ConsoleMiners;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ConsoleMiners
{
    public class Treasure : Item
    {
        public Treasure()
        {
            Color = ConsoleColor.DarkCyan;
            RarityBase = 1;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 100000 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Diamond : Item
    {
        public Diamond()
        {
            Color = ConsoleColor.Cyan;
            RarityBase = 5;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 1000*getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = true;
        }
    }
    public class Emerald : Item
    {
        public Emerald()
        {
            Color = ConsoleColor.Green;
            RarityBase = 3;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 2000 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Jade : Item
    {
        public Jade()
        {
            Color = ConsoleColor.DarkGreen;
            RarityBase = 20;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 500 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Quartz : Item
    {
        public Quartz()
        {
            Color = ConsoleColor.Gray;
            RarityBase = 160;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 10*getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Amethyst : Item
    {
        public Amethyst()
        {
            Color = ConsoleColor.Magenta;
            RarityBase = 40;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 20 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Gold : Item
    {
        public Gold()
        {
            Color = ConsoleColor.Yellow;
            RarityBase = 5;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 100 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Ruby : Item
    {
        public Ruby()
        {
            Color = ConsoleColor.Red;
            RarityBase = 6;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 90 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = true;
        }
    }
    public class Copper : Item
    {
        public Copper()
        {
            Color = ConsoleColor.DarkRed;
            RarityBase = 30;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 75 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Pyrite : Item
    {
        public Pyrite()
        {
            Color = ConsoleColor.DarkYellow;
            RarityBase = 70;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 20 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Coal : Item
    {
        public Coal()
        {
            Color = ConsoleColor.DarkGray;
            RarityBase = 120;
            Rarity = (int)(RarityBase * Item.ChanceBoosters[Name]);
            Value = 15 * getConditionMultiplier() * Item.ValueBoosters[Name];
            useA = false;
        }
    }
    public class Item
    {
        //The list of chance boosters that the player has obtained, containing the name of the item it's for, and the multiplier for it's chance.
        public static Dictionary<string, float> ChanceBoosters { get; set; }
        //The list of value boosters that the player has obtained, containing the name of the item it's for, and the multiplier for it's value.
        public static Dictionary<string, float> ValueBoosters { get; set; }
        //When the name of this item is displayed it will be in this color.
        public ConsoleColor Color { get; set; }
        //The possible values for the condition of an item.
        public enum ConditionEnum { Perfect, Good, Okay, Bad, Horrible }
        //The smaller this number is, the smaller the chance of finding the item is. This value is the base value of the "Rarity" variable which will be the one being accessed.
        public int RarityBase { get; set; }
        //This will be the value being accessed for getting the rarity of the item. It is set to the RarityBase times the chance multiplier if it's available.
        public int Rarity { get; set; }
        //The amount of money the player will get for selling this item. It will vary based on the value boosters, and the condition.
        public virtual double Value { get; set; }
        //The condition of the item.
        public ConditionEnum Condition { get; set; }
        //The name of the item found. ex. Gold, Copper, Diamond.
        public string Name { get; set; }
        //Whether you use the letter "a" in a sentence such as you found *blank*. ex. You found iron. You found a diamond. 
        public bool useA { get; set; }
        public bool CanChangeValue { get; set; }
        public bool CanChangeChance { get; set; }


        //Get the name of the item with "a " in front of it depending on if "useA" is set to true.
        public string CorrectForm()
        {
            if (useA)
                return "a " + Name;

            return Name;
        }

        //Get the multiplier for the value of an item depending on it's "Condition" variable.
        public float getConditionMultiplier()
        {
            switch(Condition)
            {
                case ConditionEnum.Horrible:
                    return 0.25f;
                case ConditionEnum.Bad:
                    return 0.75f;
                case ConditionEnum.Okay:
                    return 1f;
                case ConditionEnum.Good:
                    return 1.5f;
                case ConditionEnum.Perfect:
                    return 2f;
            }
            return 1f;
        }


        public Item()
        {
            System.Random random = new System.Random();

            //Give the item a random condition.
            Condition = (ConditionEnum)random.Next(0, 5);

            //Set the name variable of the item to the name of it's class to avoid unnecessary code.
            Name = GetType().Name;
        }

        //removes the item from the player's inventory and add the item's value to the "Money" variable.
        public void Sell()
        {
            Player.Money += Value;
            Player.Inventory.Remove(this);
        }
    }

    class Ability
    {
        //Name of the ability to display
        public string Name { get; set; }
        //Whether the player has been purchased it yet or not.
        public bool Purchased { get; set; }
        //The deduction of money for buying this ability.
        public double Price { get; set; }
        public Ability(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
            Purchased = false;
        }
    }
    class Player
    {
        //Sets the console color to the item's "Color" variable, print's the item's name, then sets the console color back to default. Use this to display the item.
        public static void WriteInItemColor(Item item, string text)
        {
            Console.ForegroundColor = item.Color;
            Console.Write(text);
            Console.ForegroundColor = DefaultConsoleColor;
        }

        public static void WriteInItemColor(Type type, string text)
        {
            Item item = (Item)Activator.CreateInstance(type);
            Console.ForegroundColor = item.Color;
            Console.Write(text);
            Console.ForegroundColor = DefaultConsoleColor;
        }

        //Gets a new price for the player to pay for another chance booster base on the item's rarity, and value.
        public static double GetPriceForNewChanceBooster(Type ItemType)
        {
            //Create a new item of this type
            Item item = (Item)Activator.CreateInstance(ItemType);
            //Gets the sum of every item's RarityBase and divides it by this item's RarityBase. The smaller this item's RarityBase, the larger this initial price is.
            decimal initialPrice = GetPossibleItemTypes().Select(x => { Item inst = (Item)Activator.CreateInstance(x); return inst.RarityBase; }).Sum() / (decimal)item.RarityBase;
            //The current boost value of this item.
            decimal CurrentBoost = (decimal)Item.ChanceBoosters.Single(x => x.Key == ItemType.Name).Value;
            //The final price.
            decimal result = (decimal)(initialPrice * 10) * CurrentBoost;
            return (double)result;
        }

        //Gets a new price for the player to pay for another value booster base on the item's rarity, and value.
        //This method is very similar to the GetPriceForNewChanceBooster method.
        public static double GetPriceForNewValueBooster(Type ItemType)
        {
            //Create a new item of this type
            Item item = (Item)Activator.CreateInstance(ItemType);
            //Gets the sum of every item's RarityBase and divides it by this item's RarityBase. The smaller this item's RarityBase, the larger this initial price is.
            decimal initialPrice = GetPossibleItemTypes().Select(x => { Item inst = (Item)Activator.CreateInstance(x); return inst.RarityBase; }).Sum() / (decimal)item.RarityBase;
            //The current boost value of this item type.
            decimal CurrentBoost = (decimal)Item.ValueBoosters.Single(x => x.Key == ItemType.Name).Value;
            //The final price
            decimal result = (decimal)(initialPrice * 10) * CurrentBoost;
            return (double)result;
        }

        //Gets a list of every possible type of item you can get in the game.
        static List<Type> GetPossibleItemTypes()
        {
            return Assembly.GetAssembly(typeof(Item)).GetTypes().Where(x => x.IsSubclassOf(typeof(Item))).ToList();
        }
        //Takes a list of item types and sorts it rarity ascending.
        static List<Type> SortItemTypesByRarity(List<Type> list)
        {
            return list.OrderBy(x => { Item i = (Item)Activator.CreateInstance(x); return i.Rarity; }).ToList();
        }
        //Gets the type of item to give to the player based on their rarities.
        static Type GetItem()
        {
            System.Random random = new System.Random();
            List<Type> ItemTypes = SortItemTypesByRarity(GetPossibleItemTypes());

            //If the "Remove quarts and coal" ability iss purchased, remove them from the list of items to give to the player.
            if (Abilities[2].Purchased)
            {
                ItemTypes.Remove(typeof(Coal));
                ItemTypes.Remove(typeof(Quartz));
            }    

            //Add a list of one instance of each item to access their rarities.
            List<Item> potentialItems = new List<Item>();
            foreach (Type t in ItemTypes)
                potentialItems.Add((Item)Activator.CreateInstance(t));

            //Get one item from the potentialItems list to give to the player. The chance of each one is base on the Rarity variable of the item.
            int total = potentialItems.Sum(x => x.Rarity);
            int selectionNumber = random.Next(0, total);
            foreach (Item item in potentialItems)
            {
                if (selectionNumber <= item.Rarity)
                    return item.GetType();
            }

            //Return the type of item to give to the player.
            return potentialItems[potentialItems.Count-1].GetType();
        }
        //Calls everytime the player mines. It will either give the player a new item of find nothing. It will diplsy the results to the player.
        static void Mine()
        {
            System.Random random = new System.Random();
            //If there is still a chance of getting nothing and the player has purchased the "Never get Nothing" ability is purchased, set the NothingChance to 5 so that you always find something.
            if (NothingChance != 5 && Abilities[3].Purchased)
                NothingChance = 5;

            //If nothing chance is 3 then you have a 25% chance of getting something. If it's 5 then 0%.
            if (random.Next(0, 4) != NothingChance)
            {
                Item item = (Item)Activator.CreateInstance(GetItem());
                Item DoubledItem = new Item();

                //If the "Double reward" ability is active then duplicate the object and add it to the inventory.
                if (Abilities[0].Purchased)
                {
                    DoubledItem = (Item)Activator.CreateInstance(item.GetType());
                    DoubledItem.Condition = item.Condition;
                    Inventory.Add(DoubledItem);
                }


                Inventory.Add(item);

                //The following segment displays the info of the item found to the player.
                Console.Write($"You found ");
                WriteInItemColor(item, item.CorrectForm());
                //If the "Double Reward" ability is active, double the item that was found.
                if (Abilities[0].Purchased)
                    Console.Write(" x2");
                Console.Write($". Condition: {item.Condition}, Value: ");
                Console.ForegroundColor = ConsoleColor.Green;

                //If the "Double Reward" ability is active, print the doubled price.
                if (Abilities[0].Purchased)
                    Console.Write("$" + printDouble(item.Value + DoubledItem.Value) + "\n");
                else
                    Console.Write("$" + printDouble(item.Value) + "\n");
                Console.ForegroundColor = DefaultConsoleColor;
            }
            else
            {
                Console.WriteLine($"You found nothing.");
            } 
        }
        //Lists all of the items in the player's inventory, and their values.
        static void DisplayInventory()
        {
            //If the inventory is not empty, display the index of it in the inventory, it's name, and it's value.
            if (Inventory.Count > 0)
            {
                int i = 1;
                Console.WriteLine("\n~Inventory~\n[");
                foreach (Item item in Inventory)
                {
                    Console.Write($"{i}.");
                    WriteInItemColor(item, item.Name);
                    Console.Write($": ${ printDouble(item.Value)}\n");
                    i++;
                }
                Console.WriteLine("]\n");
            }
            else
                Console.WriteLine("Your inventory is empty.");
        }

        //Print's a double in the desired format with only two decimal places displayed.
        static string printDouble(double d)
        {
            return string.Format("{0:N2}", d);
        }

        //Trim the player's input and convert it to lower case.
        static void ToWorkableFormat(ref string input)
        {
            input = input.Trim().ToLower();
        }

        //Display the categories of the market and get the selection of the player's choice.
        static void GetMarketCategorySelection()
        {
            string selection = Console.ReadLine();
            ToWorkableFormat(ref selection);
            Console.Write("Your Balance: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("$" + printDouble(Money) + "\n");
            Console.ForegroundColor = DefaultConsoleColor;

            //If the player did not type cancel
            if (selection != "cancel" && selection != "4")
            {
                //Match the player's input with the section of the market, and excecute the correct method.
                switch (selection)
                {
                    case "chance boosters":
                    case "chance":
                    case "1":
                        MarketChanceBoosters();
                        break;
                    case "value boosters":
                    case "value":
                    case "2":
                        MarketValueBoosters();
                        break;
                    case "abilities":
                    case "3":
                        MarketAbilities();
                        break;
                    default:
                        //Bring them back to the beginning of the selection.
                        PrintIncorrectFormatMessage();
                        GetMarketCategorySelection();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nCanceled.");
            }
        }

        //Give feedback to the player if they gave incorrect input.
        static void PrintIncorrectFormatMessage()
        {
            Console.WriteLine("Error: Incorrect format");
            Console.Beep();
        }

        //Prints text in green if the cost is equal to or less than the player's money, or else, it will print in red.
        static void WriteByAffordColor(double cost, string text)
        {
            if (Money >= cost)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(text);

            Console.ForegroundColor = DefaultConsoleColor;
        }

        //Display all of the available chance boosters and their prices. Get the player's selection and give the booster to the player.
        static void MarketChanceBoosters()
        {
            Console.WriteLine("\nChance Boosters\n");
            List<Type> items = SortItemTypesByRarity(GetPossibleItemTypes());
            int i = 1;
            //Display the name of each item and how much it would cost to buy a booster for it.
            foreach (Type item in items)
            {
                Console.Write($"{i}. ");
                WriteInItemColor(item, item.Name);
                Console.Write(" - ");
                WriteByAffordColor(GetPriceForNewChanceBooster(item), $"${printDouble(GetPriceForNewChanceBooster(item))}\n");
                i++;
            }

            Console.WriteLine("-Back\n\nType your selection...");
            getSelection();

            //Get the player's selection of what booster they would like to purchase.
            void getSelection()
            {
                //Get the input and put it into the correct format.
                string selection = Console.ReadLine();
                ToWorkableFormat(ref selection); 

                //If the player types "back", give the main market selection.
                if (selection == "back")
                {
                    printMarketOptions();
                    GetMarketCategorySelection();
                    return;
                }

                //If the player's input is not the name of an item, restart the selection.
                if (!GetPossibleItemTypes().Select(x => x.Name.ToLower()).ToList().Contains(selection))
                {
                    if (IsDigits(selection))
                    {
                        int selectionIndex = Convert.ToInt32(selection)-1;

                        if (selectionIndex < items.Count && selectionIndex >= 0)
                        {
                            if (GetPriceForNewChanceBooster(items[selectionIndex]) <= Money)
                            {
                                BuyChanceBooster(items[selectionIndex]);
                                MarketChanceBoosters();
                            }
                            else
                            {
                                Console.WriteLine("You do not have enough money for this purchase.");
                                getSelection();
                            }
                            return;
                        }
                    }

                    PrintIncorrectFormatMessage();
                    getSelection();
                    return;
                }

                //get the type of item that matches the player's input.
                Type type = GetPossibleItemTypes().Single(x => x.Name.ToLower() == selection);

                //If the palyer can afford to buy the booster, increase the chance booster multiplier by 0.5 and take the required cost from the player's money.
                if (GetPriceForNewChanceBooster(type) <= Money)
                {
                    BuyChanceBooster(type);
                    MarketChanceBoosters();
                    return;
                }
                else
                {
                    //Inform the player that they do not have enough money to buy this booster, and restart the selection.
                    Console.WriteLine("You do not have enough money for this purchase.");
                    getSelection();
                }
            }
        }

        static void BuyChanceBooster(Type type)
        {
            Money -= GetPriceForNewChanceBooster(type);
            Item.ChanceBoosters[type.Name] = Item.ChanceBoosters[type.Name] + 0.5f;
            Console.WriteLine($"\nYou have raised your profit multiplier when getting {type.Name.ToLower()} to {Item.ChanceBoosters[type.Name]}");
        }

        static bool IsDigits(string Input)
        {
            if (Input == "")
                return false;

            foreach (char c in Input)
            {
                if (c < '0' || c > 'p')
                    return false;
            }
            return true;
        }

        //Display all of the available value boosters and their prices. Get the player's selection and give the booster to the player.
        //This method is very similar to the "MarketChanceBoosters" method.
        static void MarketValueBoosters()
        {
            Console.WriteLine("\nValue Boosters\n");
            List<Type> items = SortItemTypesByRarity(GetPossibleItemTypes());
            int i = 1;
            foreach (Type item in items)
            {
                Console.Write($"{i}. ");
                WriteInItemColor(item, item.Name);
                Console.Write(" - ");
                WriteByAffordColor(GetPriceForNewValueBooster(item), $"${printDouble(GetPriceForNewValueBooster(item))}\n");
                i++;
            }

            Console.WriteLine("-Back\n\nType your selection...");
            getSelection();


            void getSelection()
            {
                string selection = Console.ReadLine();
                ToWorkableFormat(ref selection);
                if (selection == "back")
                {
                    printMarketOptions();
                    GetMarketCategorySelection();
                    return;
                }
                if (!GetPossibleItemTypes().Select(x => x.Name.ToLower()).ToList().Contains(selection))
                {
                    //Check to see if it's an index selection
                    if (IsDigits(selection))
                    {
                        int selectionIndex = Convert.ToInt32(selection)-1;
                        if (selectionIndex < items.Count && selectionIndex >= 0)
                        {
                            if (GetPriceForNewValueBooster(items[selectionIndex]) <= Money)
                            {
                                BuyValueBooster(items[selectionIndex]);
                                MarketValueBoosters();
                            }
                            else
                            {
                                Console.WriteLine("You do not have enough money for this purchase.");
                                getSelection();
                            }
                            return;
                        }
                    }


                    PrintIncorrectFormatMessage();
                    getSelection();
                    return;
                }
                Type type = GetPossibleItemTypes().Single(x => x.Name.ToLower() == selection);
                if (GetPriceForNewChanceBooster(type) <= Money)
                {
                    BuyValueBooster(type);
                    MarketValueBoosters();
                    return;
                }
                else
                {
                    Console.WriteLine("You do not have enough money for this purchase.");
                    getSelection();
                }
            }
        }

        static void BuyValueBooster(Type type)
        {
            Money -= GetPriceForNewValueBooster(type);
            Item.ValueBoosters[type.Name] = Item.ValueBoosters[type.Name] + 0.5f;
            Console.WriteLine($"\nYou have raised your profit multiplier when getting {type.Name.ToLower()} to {Item.ValueBoosters[type.Name]}");
        }

        static void printMarketOptions()
        {
            Console.WriteLine("\n~Market~\n1.Chance Boosters\n2.Value Boosters\n3.Abilities\n4.Cancel\n\nType Your Selection...");
        }

        //Display all of the available abilities and their prices. Get the player's selection and give the ability to the player.
        //This method works the same as the "MarketValueBoosters()" and "MarketChanceBoosters()" methods.
        static void MarketAbilities()
        {
            Console.WriteLine("\nAbilities\n");
            int i = 1;
            foreach (Ability ability in Abilities)
            {
                Console.Write("\n-");
                Console.Write($"{i}. {ability.Name} - ");
                if (ability.Purchased)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Purchased");
                    Console.ForegroundColor = DefaultConsoleColor;
                }
                else
                {
                    WriteByAffordColor(ability.Price, "$"+printDouble(ability.Price));
                }
                i++;
            }
            Console.WriteLine("\n-Back\n\nType your selection...");
            getSelection();
            void getSelection()
            {
                string selection = Console.ReadLine();
                ToWorkableFormat(ref selection);
                if (selection == "back")
                {
                    printMarketOptions();
                    GetMarketCategorySelection();
                    return;
                }
                if (!Abilities.Select(x => x.Name.ToLower()).ToList().Contains(selection))
                {
                    if (IsDigits(selection))
                    {
                        int selectionIndex = Convert.ToInt32(selection)-1;

                        if (selectionIndex < Abilities.Count && selectionIndex >= 0)
                        {
                            Ability selectedAbility = Abilities[selectionIndex];
                            if (!selectedAbility.Purchased)
                            {
                                if (selectedAbility.Price <= Money)
                                {
                                    BuyAbility(selectedAbility);
                                    MarketAbilities();
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("You do not have enough money for this purchase.");
                                    getSelection();
                                    return; //Experimental
                                }
                            }
                            else
                            {
                                Console.WriteLine("You have already purchased this ability. Try something else.");
                                getSelection();
                                return;
                            }
                        }
                    }
                    PrintIncorrectFormatMessage();
                    getSelection();
                    return;
                }
                Ability ability = Abilities.Single(x => x.Name.ToLower() == selection);
                if (!ability.Purchased)
                {
                    if (ability.Price <= Money)
                    {
                        BuyAbility(ability);
                        MarketAbilities();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough money for this purchase.");
                        getSelection();
                        return; //Experimental
                    }
                }
                else
                {
                    Console.WriteLine("You have already purchased this ability. Try something else.");
                    getSelection();
                    return;
                }
            }
        }

        static void BuyAbility(Ability ability)
        {
            Money -= ability.Price;
            ability.Purchased = true;
            Console.WriteLine($"\nYou have purchased the Ability: " + ability.Name + ".");
        }

        //Display all of the commands to the player when they print "help".
        static void PrintHelp()
        {
            Console.WriteLine("\n~Help~\nMine = m or mine\nView Inventory = inv\nSell Items = sell\nView Money = money\nView Market = market");
        }

        //Print's the given text in the center of the screen. Used for the title
        static void PrintInCenter(string Text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - Text.Length) / 2));
            Console.WriteLine(Text);
        }

        //The list of all items in the player's inventory.
        static public List<Item> Inventory;
        //The money that the player has.
        static public double Money = 0;
        //Set's the default color that all normal text will display in.
        static public ConsoleColor DefaultConsoleColor = ConsoleColor.White;
        //3 = has a 1 in 4 chance of getting notheing when mining. 5 = no chance.
        static public int NothingChance = 3;
        //All of the abilities the player has bought.
        static public List<Ability> Abilities;

        static void Main(string[] args)
        {
            //Display the title and help info when the application is started.
            Console.Title = "Console Miners";
            Console.ForegroundColor = DefaultConsoleColor;
            PrintInCenter("~~~~~~~~~~~~~~~~~~~~~~~~ Welcome To Console Miners! ~~~~~~~~~~~~~~~~~~~~~~~~\n");
            PrintInCenter("version: 1.1\n");
            PrintHelp();

            //Initializes the lists and abilities.
            Item.ChanceBoosters = new Dictionary<string, float>();
            Item.ValueBoosters = new Dictionary<string, float>();
            Inventory = new List<Item>();
            Abilities = new List<Ability>() { new Ability("Double Reward", 25000), new Ability("Double Mining", 30000), new Ability("Eliminate Quartz and Coal", 40000), new Ability("Always Find Something", 10000)};
            
            //Set's the chance booster of all items to one by default.
            foreach (Type type in GetPossibleItemTypes())
            {
                Item.ChanceBoosters.Add(type.Name, 1f);
                Item.ValueBoosters.Add(type.Name, 1f);
            }

            //Reads any commands the player will give and call all necessary actions, until the application is exited.
            while (true)
            {
                //Gets the input of the player and puts it into the required format.
                string input = Console.ReadLine();
                ToWorkableFormat(ref input);

                //The following if statements check if the input is any of the available commands and executes any actions.
                if (input == "mine" || input == "m")
                {
                    Mine();

                    //Mine again if the player has purchased the double mine ability.
                    if (Abilities[1].Purchased)
                        Mine();
                }
                if (input == "inv")
                {
                    DisplayInventory();
                }
                if (input == "help")
                {
                    PrintHelp();
                }
                if (input == "market")
                {
                    printMarketOptions();
                    GetMarketCategorySelection();
                    
                }
                if (input == "sell")
                {
                    //Display all items in the player's inventory and get the selection of which one to sell.
                    if (Inventory.Count > 0)
                    {
                        DisplayInventory();

                        //Get the index of which item to sell
                        Console.WriteLine("Give the number of the item you would like to sell or \"all\" to sell all.");
                        string sellNum = Console.ReadLine();
                        ToWorkableFormat(ref sellNum);
                        if (Char.IsDigit(sellNum.ToCharArray()[0]) && Convert.ToInt32(sellNum) > 0 && Convert.ToInt32(sellNum) <= Inventory.Count)
                        {
                            Item soldItem = Inventory[Convert.ToInt32(sellNum) - 1];
                            Console.WriteLine($"You sold {soldItem.CorrectForm()} for ${printDouble(soldItem.Value)}.");
                            soldItem.Sell();
                        }
                        else
                        {
                            //Sell every item in the inventory if so requested by the player.
                            if (sellNum == "all")
                            {
                                foreach (Item item in Inventory.ToList())
                                    item.Sell();

                                Console.WriteLine($"You sold everything.");
                            }
                            else
                            {
                                PrintIncorrectFormatMessage();
                            }
                        }
                    }
                    else
                        Console.WriteLine("Your inventory is empty.");
                }
                if (input == "money")
                {
                    //Display the money that the player has.
                    Console.Write($"\nYour Balance: ");
                    WriteByAffordColor(Money, "$"+printDouble(Money)+"\n");
                }
            }    
        }
    }
}