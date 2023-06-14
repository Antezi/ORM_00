using Avalonia.Controls;
using Avalonia.Interactivity;
using ORM_00.Classes;
using ORM_00.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM_00;

public partial class OKRWindow : Window
{
    public OKRWindow()
    {
        InitializeComponent();
        InitElements();
        loadData();
    }

    public void InitElements()
    {
        backButt.Click += BackButt_Click;
        HisButt.Click += HisButt_Click;
        searchBox.AddHandler(KeyUpEvent, SearchBox_KeyUp, Avalonia.Interactivity.RoutingStrategies.Tunnel);
        sortBox.SelectionChanged += SortBox_SelectionChanged;
        filtBox.SelectionChanged += FiltBox_SelectionChanged;
        loadData();
    }

    private void HisButt_Click(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void FiltBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        loadData();
    }

    private void SortBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        loadData();
    }

    private void SearchBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        loadData();
    }

    //���������� ��������
    private void DelButt_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int Rem = (int)(sender as Button).Tag;
        DBClass.db.Okps.Remove(DBClass.db.Okps
                                                    .Find(Rem));
        DBClass.db.SaveChanges();
        loadData();
    }

    private void GoIn(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        StageWindow stageWindow = new StageWindow();
        stageWindow.Show();
        this.Close();
    }



    private void BackButt_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    public void loadData()
    {
        //���� � �������� ���������
        List<Okp> okps = new List<Okp>();
        //����������� �����
        okps = DBClass.db.Okps.ToList();
        List<OKRClass> OKRClasses = new List<OKRClass>();
        int count;

        //���������� ������
        string search = searchBox.Text ?? "";
        if (!string.IsNullOrEmpty(search))
        {
            okps = okps.Where(x => x.Name.Trim()
                                                        .ToLower()
                                                        .Contains(search
                                                        .Trim()
                                                        .ToLower()) ||
                                                        x.Namecomponent.Trim()
                                                                            .ToLower()
                                                                            .Contains(search
                                                                            .Trim()
                                                                            .ToLower())).ToList();
        }

        //���������� ����������
        switch (sortBox.SelectedIndex)
        {
            case 0:
                okps = okps.ToList();
                break;

            case 1:
                okps = okps.OrderByDescending(x => x.Name)
                                                                         .ToList();
                break;
            case 2:
                okps = okps.OrderBy(x => x.Name)
                                                               .ToList();
                break;
        }

        //���������� ����������
        switch (filtBox.SelectedIndex)
        {



        }

        foreach (Okp okp in okps)
        {
            OKRClasses.Add(new OKRClass
            {

                IdOKR = $"Id: {okp.Id}",
                Title = $"������������: {okp.Name}",

                NameComp = $"������������ ���������: {okp.Namecomponent}",

            });
        }

        //����� ���-�� ������
        listBox.Items = OKRClasses.ToList();
        countBlock.Text = $"{okps.Count} �� {DBClass.db.Okps.Count()}";

    }

    //private string GetColor(string status)
    //{
    //    switch (status)
    //    {
    //        case "��������":
    //            return "White";
    //        case "�������":
    //            return "Green";
    //        case "���������������":
    //            return "Grey";
    //        case "�������":
    //            return "Red";
    //    }
    //}

    private string GetManufacturer(int productmanufacturer)
    {
        switch (productmanufacturer)
        {
            case 1:
                return "�������������: �500";

            case 2:
                return "�������������: ���������";

            case 3:
                return "�������������: Knauf";

            case 4:
                return "�������������: MixMaster";

            case 5:
                return "�������������: ���";

            case 6:
                return "�������������: �����";

            case 7:
                return "�������������: Vinylon";

            case 8:
                return "�������������: ���������� ����� ";

            case 9:
                return "�������������: Weber";

            case 10:
                return "�������������: Hesler";

            case 11:
                return "�������������: Armero";

            case 12:
                return "�������������: Wenzo Roma";

            case 13:
                return "�������������: KILIMGRIN";

            case 14:
                return "�������������: �����";

            case 15:
                return "�������������: RUIZ";

            case 16:
                return "�������������: Husqvarna";

            case 17:
                return "�������������: Delta";
        }
        return "";
    }
}