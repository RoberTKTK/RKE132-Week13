﻿using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());
FindCustomer();

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;

}


static void ReadData(SQLiteDataReader myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT custmoer.firstName, customer.lastName, status.statustype " + 
        "JOIN customer on customer.rowid = customerStatus.customerId " +
        "JOIN status on status.rowid = customerStatus.statusId";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader.GetString(0);
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);


        Console.WriteLine($"Full name: {readerStringFirstName}{readerStringLastName}; DOB: {readerStringDoB}");
    }

    myConnection.Close();
}

static void InstertCustomer(SQLiteConnection connection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = connection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{dob}')";
    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted:  {rowInserted}");

   
    ReadData(myConnection);
}


static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}

static void FindCustomer();
{

}