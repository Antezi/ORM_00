using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ORM_00.Classes;
using ORM_00.Database;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ORM_00;

public partial class StageWindow : Window
{
    public StageWindow()
    {
        InitializeComponent();
        InitElementss();
        loadDatas();
    }

    public void InitElementss()
    {
        backButt.Click += BackButt_Click;
        HisButt.Click += HisButt_Click;
        searchBox.AddHandler(KeyUpEvent, SearchBox_KeyUp, Avalonia.Interactivity.RoutingStrategies.Tunnel);
        sortBox.SelectionChanged += SortBox_SelectionChanged;
        filtBox.SelectionChanged += FiltBox_SelectionChanged;
        loadDatas();
    }

    private void HisButt_Click(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void FiltBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        loadDatas();
    }

    private void SortBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        loadDatas();
    }

    private void SearchBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        loadDatas();
    }

    //���������� ��������
    private void DelButt_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int Rem = (int)(sender as Button).Tag;
        DBClass.db.Okps.Remove(DBClass.db.Okps
                                                    .Find(Rem));
        DBClass.db.SaveChanges();
        loadDatas();
    }

    //private void EditButt_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    //{
    //    AddEditWindow addEditWindow = new AddEditWindow();
    //    addEditWindow.Show();
    //    this.Close();
    //}



    private void BackButt_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    public void loadDatas()
    {
        //���� � �������� ���������
        List<Stage> stages = new List<Stage>();
        //����������� �����
        stages = DBClass.db.Stages.ToList();
        List<STGclass> STGclasses = new List<STGclass>();
        int count;

        //���������� ������
        string search = searchBox.Text ?? "";
        if (!string.IsNullOrEmpty(search))
        {
            stages = stages.Where(x => x.Stagename.Trim()
                                                        .ToLower()
                                                        .Contains(search
                                                        .Trim()
                                                        .ToLower())).ToList();

                                                        //||
                                                        //x.Numberstage.Trim()
                                                        //                    .ToLower()
                                                        //                    .Contains(search
                                                        //                    .Trim()
                                                        //                    .ToLower())).ToList();
        }

        //���������� ����������
        switch (sortBox.SelectedIndex)
        {
            case 0:
                stages = stages.ToList();
                break;

            case 1:
                stages = stages.OrderByDescending(x => x.Stagename)
                                                                         .ToList();
                break;
            case 2:
                stages = stages.OrderBy(x => x.Stagename)
                                                               .ToList();
                break;
        }

        //���������� ����������
        switch (filtBox.SelectedIndex)
        {



        }

        foreach (Stage stage in stages)
        {
            STGclasses.Add(new STGclass
            {

                Id = $"Id: {stage.Idstage}",
                Idok = $"������������: {stage.Idokp}",

                Numberstg = $"������������ ���������: {stage.Numberstage}",

                Stagenm = $"������������ ���������: {stage.Stagename}",

                Numberofproduct = $"������������ ���������: {stage.Numberofproducts}",

                Startdy = $"������������ ���������: {stage.Startday}",

                Enddy = $"������������ ���������: {stage.Endday}",

            });
        }

        //����� ���-�� ������
        listBox.Items = STGclasses.ToList();
        countBlock.Text = $"{stages.Count} �� {DBClass.db.Stages.Count()}";

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