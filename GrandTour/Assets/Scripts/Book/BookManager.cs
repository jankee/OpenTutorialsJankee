using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class BookManager : MonoBehaviour
{

    //public Book[] books = new Book[4];

    public static List<Book> library = new List<Book>();

    MainClass instance = new MainClass();

    public string book = "";
    public string auther = "";
    public string year = "";

    private bool loop = false;

    string[] index = new string[3];

    string[] contants = new string[3];

    int count = 0;

    // Use this for initialization
    void Start()
    {


    }

    public void Menu(int i)
    {

        int choice = i;

        if (choice == 1)
        {
            print("1");
            loop = true;
        }
        else if (choice == 2)
        {
            print("2");

            SaveToFile();
        }
        else if(choice == 3)
        {
            ReadFromFile();
            
        }
        else if (choice == 4)
        {
            foreach (Book item in library)
            {
                print(item._ToString());    
            }
            
        }

        //if (i == 1)
        //{
        //Environment.Exit(0);
        //    book = txt;
        //    print(book + " " + MainClass.mainI);
        //}
        //else if (i == 2)
        //{
        //    auther = txt;
        //    print(auther + " " + MainClass.mainI);
        //}
        //else if (i == 3)
        //{
        //    year = txt;

        //    print(year + book + auther);

        //    AddBook();
        //}
    }

    //books[0] = new Book("Let the Right One In", "John Lindqvist", 2004);
    //books[1] = new Book("Game of Thrones", "George R.R", 1996);
    //books[2] = new Book("A Clash of King", "George R.R", 1998);
    //books[3] = new Book("The amber Spyglass", "Philip Pullman", 2000);
    //}

    public void GetMenuChoice()
    {
        MainClass.testText.text = "1. Add a book \n2. List books\n3. Quit";

        print("1. Add a book");
        print("2. List books");
        print("3. Quit");

        //string choice = instance.TextInput();

        //int numChoice = Convert.ToInt32(choice);

    }

    public void AddBook(string contant)
    {
        if (loop)
        {
            index[0] = "Title";
            index[1] = "Auther";
            index[2] = "Year";

            int indexCount = count;

            if (count == 3)
            {
                print(contants[0] + contants[1] + contants[2]);
                Book b = new Book(contants[0], contants[1], contants[2]);

                print(b._ToString());

                library.Add(b);

                count = 0;

                indexCount = count;
            }

            if (count < 3)
            {
                print(count);

                if (indexCount < 2)
                {
                    indexCount += 1;      
                }
                else
                {
                    indexCount = 0;
                }

                MainClass.testText.text = index[indexCount];

                contants[count] = contant;

                MainClass.TextEmpty();

                
                count++;
            }

            print(library.Count);
        }
    }

    public void ListBooks()
    {
        print("ListBooks");
        foreach (Book item in library)
        {
            print(item._ToString());

            //MainClass.TestText1.text = item._ToString();
        }

        print(library.Count);
    }

    private void SaveToFile()
    {
        print("save file");
        StreamWriter writer = new StreamWriter(@"\Books.txt");

        foreach(Book b in library)
        {
            string output = b.GetTitle() + "\t" + b.GetAuther() + "\t" + b.GetYear();
            writer.WriteLine(output);
        }

        writer.Close();

        print("Saved file");
    }

    private void ReadFromFile()
    {
        StreamReader reader = new StreamReader(@"\Books.txt");

        string s = reader.ReadLine();

    

        while (s != null)
        {
            print("ReadFromFile");
            char[] delimiter = { '\t' };
            string[] fields = s.Split(delimiter);
            library.Add(new Book(fields[0], fields[1], fields[2]));
            print(library.Count);
            s = reader.ReadLine();
        }
    }
}
