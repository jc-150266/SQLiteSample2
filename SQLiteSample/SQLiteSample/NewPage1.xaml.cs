
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SQLiteSample
{
    public partial class NewPage1 : ContentPage
    {
        /*public NewPage1()
        {
            MainPage = new MyPage();
        }*/

        readonly TodoRepository _db = new TodoRepository(); // <-1
        static readonly object Locker = new object();

        private ObservableCollection<TodoRepository> ar;

        public NewPage1()
        {

            var listView = new ListView
            { // <-2
                ItemsSource = _db.GetItems(), // <-3
                ItemTemplate = new DataTemplate(typeof(TextCell)) // <-4
            };
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Text");
            listView.ItemTemplate.SetBinding(TextCell.DetailProperty, new Binding("CreatedAt", stringFormat: "{0:yyy/MM/dd hh:mm}"));
            /*listView.ItemTapped += async (s, a) =>
            { // <-5
                var item = (TodoItem)a.Item;
                if (await DisplayAlert("削除して宜しいですか", item.Text, "OK", "キャンセル"))
                {
                    item.Delete = true; // 削除フラグを有効にして
                    _db.SaveItem(item); // データベースの更新
                    listView.ItemsSource = _db.GetItems(); // リスト更新 
                }
            }*/
            var entry = new Entry
            { // <-6
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            var buttonAdd = new Button
            { // <-7
                WidthRequest = 60,
                TextColor = Color.White,
                Text = "Add"
            };
            buttonAdd.Clicked += (s, a) =>
            { // <-8
                if (!String.IsNullOrEmpty(entry.Text))
                { // Entryに文字列が入力されている場合に処理する
                    var item = new TodoItem { Text = entry.Text, CreatedAt = DateTime.Now, Delete = false };
                    _db.SaveItem(item);
                    listView.ItemsSource = _db.GetItems(); //リスト更新
                    entry.Text = ""; // 入力コントロールをクリアする
                }
            };

            Content = new StackLayout
            { // <-9
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children = {
          new StackLayout {
            BackgroundColor = Color.Navy, // 入力部の背景色はネイビー
            Padding = 5,
            Orientation = StackOrientation.Horizontal,
            Children = {entry, buttonAdd} // Entryコントロールとボタンコントロールを横に並べる
          },
          listView // その下にリストボックスを置く
        }
            };
        }

        public void Action(MenuItem item)
        {
            var text = item.CommandParameter;
            if (item.Text == "Delete")
            {
                ar.RemoveAt(ar.IndexOf(text));
                //UserModel.deleteUser(id);

            }
        }
        class MyCell : ViewCell
        {
            public MyCell(NewPage1 myPage)
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                //label.SetBinding(Label.TextProperty, new Binding("."));

                var actionDelete = new MenuItem
                {
                    Text = "Delete",
                    Command = new Command(p => myPage.DisplayAlert("Delete", p.ToString(), "OK")),
                    IsDestructive = true,
                };

                actionDelete.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                actionDelete.Clicked += (s, a) => myPage.Action((MenuItem)s);
                ContextActions.Add(actionDelete);

                View = new StackLayout
                {
                    Padding = 10,
                    Children = { label }
                };
            }
        }
    }
}