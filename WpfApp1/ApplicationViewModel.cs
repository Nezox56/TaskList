using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private TodoItem selectedItem;
        public ObservableCollection<TodoItem> TodoList { get; set; }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      TodoItem todoItem = new TodoItem();
                      TodoList.Insert(0, todoItem);
                      SelectedItem = todoItem;
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      TodoItem todoItem = obj as TodoItem;
                      if (todoItem != null)
                      {
                          TodoList.Remove(todoItem);
                      }
                  },
                 (obj) => TodoList.Count > 0));
            }
        }

        public TodoItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ApplicationViewModel()
        {
            TodoList = new ObservableCollection<TodoItem>
            {
                new TodoItem {Title = "Встреча", Text = "поговорить с Габеном почему не выпускает третью халву", Time = "13:00" , Today={2015, 7, 20}},
                new TodoItem {Title = "Созвон", Text = "поговорить с Илоном чтоб подогнал теслу", Time = "16:00" },
                new TodoItem {Title = "Физ. активность", Text = "поход в зал", Time = "18:00" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}