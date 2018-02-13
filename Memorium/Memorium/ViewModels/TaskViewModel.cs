using Memorium.Commands;
using Memorium.Common;
using Memorium.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Memorium.ViewModels
{
    class TaskViewModel
    {
        /// <summary>
        /// Current selected task.
        /// Using for editing and deleting.
        /// </summary>
        private static TaskModel selectedTask;

        /// <summary>
        /// Database context.
        /// </summary>
        private DataContext dataContext = new DataContext();

        /// <summary>
        /// All commands handler
        /// </summary>
        private RelayCommand command;

        /// <summary>
        /// Collection of all tasks
        /// </summary>
        public ObservableCollection<TaskModel> Tasks { get; set; }

        /// <summary>
        /// Show finished tasks flag
        /// </summary>
        private static bool showFinished = false;

        /// <summary>
        /// Show active tasks flag
        /// </summary>
        private static bool showActive = false;


        public TaskModel SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
            }
        }


        /// <summary>
        /// ViewModel constructor.
        /// </summary>
        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();

            foreach (var data in dataContext.Tasks)
            {
                if (showFinished && data.isFinished)
                {
                    Tasks.Insert(0, data);
                }
                else if (showActive && !data.isFinished)
                {
                    Tasks.Insert(0, data);
                }
                else if (!showFinished && !showActive)
                {
                    Tasks.Insert(0, data);
                }
            }
        }


        /// <summary>
        /// Navigation to CreateTask view
        /// </summary>
        public RelayCommand CreateNavigationCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    NavigationService.Instance.Navigate(typeof(Views.CreateTask));
                });
            }
        }


        /// <summary>
        /// Creating task
        /// </summary>
        public RelayCommand CreateCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    var stackPanelCollection = (obj as UIElementCollection);
                    TaskModel addedTask = new TaskModel();

                    foreach (var data in stackPanelCollection)
                    {
                        var textbox = (data as TextBox);

                        switch (textbox.Name)
                        {
                            case "title":
                                addedTask.title = textbox.Text;
                                break;

                            case "content":
                                addedTask.content = textbox.Text;
                                break;
                        }
                    }

                    addedTask.isFinished = false;
                    addedTask.Status = "Active";

                    dataContext.Add(addedTask);
                    dataContext.SaveChanges();

                    NavigationService.Instance.GoBack();
                });
            }
        }


        /// <summary>
        /// Navigation to EditTask view
        /// </summary>
        public RelayCommand EditNavigationCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    NavigationService.Instance.Navigate(typeof(Views.EditTask));
                });
            }
        }


        /// <summary>
        /// Edit task
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    var refreshableData = dataContext.Tasks.Where(t => t.id == SelectedTask.id).FirstOrDefault();
                    var collection = (obj as UIElementCollection);

                    foreach (var data in collection)
                    {
                        var textbox = (data as TextBox);

                        switch (textbox.Name)
                        {
                            case "title":
                                refreshableData.title = textbox.Text;
                                break;

                            case "content":
                                refreshableData.content = textbox.Text;
                                break;
                        }
                    }

                    SelectedTask = refreshableData;
                    dataContext.SaveChanges();

                    NavigationService.Instance.GoBack();
                });
            }
        }


        /// <summary>
        /// Delete task
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    var task = dataContext.Tasks.Where(t => t.id == SelectedTask.id).FirstOrDefault();

                    if (task != null)
                        dataContext.Tasks.Remove(task);

                    dataContext.SaveChanges();
                    NavigationService.Instance.GoBack();
                });
            }
        }

        
        /// <summary>
        /// Delete all tasks from Tasks collection
        /// </summary>
        public RelayCommand DeleteAllCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    foreach (var data in dataContext.Tasks)
                    {
                        Tasks.Remove(data);
                        dataContext.Tasks.Remove(data);
                    }

                    dataContext.SaveChanges();
                });
            }
        }


        /// <summary>
        /// Navigate to previous page
        /// </summary>
        public RelayCommand BackCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    NavigationService.Instance.GoBack();
                });
            }
        }


        /// <summary>
        /// Navigate to ShowTask view
        /// </summary>
        public RelayCommand ShowNavigationCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    var selectedItem = ((obj as ItemClickEventArgs).ClickedItem as TaskModel);

                    try
                    {
                        selectedItem.Status = SelectedTask.Status;
                    }
                    catch (NullReferenceException)
                    {
                        selectedItem.Status = selectedItem.Status;
                    }

                    SelectedTask = selectedItem;
                    NavigationService.Instance.Navigate(typeof(Views.ShowTask));
                });
            }
        }


        /// <summary>
        /// Click on item in Menu View
        /// </summary>
        public RelayCommand MenuItemClickCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    if ((obj as ItemClickEventArgs).ClickedItem is TextBlock)
                    {
                        TextBlock clickedItem = (obj as ItemClickEventArgs).ClickedItem as TextBlock;

                        switch (clickedItem.Name)
                        {
                            case "all":
                                showFinished = false;
                                showActive = false;
                                Tasks.Clear();
                                NavigationService.Instance.Navigate(typeof(Views.MainPage));
                                break;

                            case "finished":
                                showFinished = true;
                                showActive = false;
                                Tasks.Clear();
                                NavigationService.Instance.Navigate(typeof(Views.MainPage));
                                break;

                            case "active":
                                showFinished = false;
                                showActive = true;
                                Tasks.Clear();
                                NavigationService.Instance.Navigate(typeof(Views.MainPage));
                                break;
                        }
                    }
                });
            }
        }

        public RelayCommand MenuLanguageItemClickCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    ComboBoxItem language = (obj as ComboBox).SelectedItem as ComboBoxItem;
                    
                    switch(language.Name)
                    {
                        case "ruLang":
                            ApplicationLanguages.PrimaryLanguageOverride = "ru-RU";
                            CoreApplication.Exit();
                            break;

                        case "enLang":
                            ApplicationLanguages.PrimaryLanguageOverride = "en-US";
                            CoreApplication.Exit();
                            break;
                    }
                });
            }
        }


        /// <summary>
        /// Finish selected task from ShowView
        /// </summary>
        public RelayCommand FinishCurrentTaskCommand
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    var currentTask = dataContext.Tasks.Where(t => t.id == SelectedTask.id).FirstOrDefault();

                    currentTask.isFinished = true;
                    currentTask.Status = "Finished";
                    (obj as TextBlock).Text = "Finished";

                    SelectedTask = currentTask;
                    dataContext.SaveChanges();
                });
            }
        }


        /// <summary>
        /// Finish tasks from MainPage view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Finish(object sender, RoutedEventArgs e)
        {
            // taking value from checkbox in view (maybe not good idea to do like this...)
            var checkBox = e.OriginalSource as CheckBox;
            SelectedTask = (checkBox.Parent as Grid).DataContext as TaskModel;

            var checkedTask = dataContext.Tasks.Where(t => t.id == SelectedTask.id).FirstOrDefault();

            if ((bool)checkBox.IsChecked)
            {
                checkedTask.isFinished = true;
                checkedTask.Status = "Finished";
            }
            else
            {
                checkedTask.isFinished = false;
                checkedTask.Status = "Active";
            }

            dataContext.SaveChanges();
            SelectedTask = checkedTask;
        }
    }
}
