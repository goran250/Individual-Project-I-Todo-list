Todolist application

This a todo list application called TodoList. The namespace is called Project_I_Todo_list. 
The is a console app with a text based user interface that can be accessed through the command-line.
The application will allow a user to create new projects and create tasks that belongs to a certain project. 
Tasks have the following properties, title, description, due date and status (Not done or Done).

When the user is using the app, the user should be able to also list, edit and remove tasks. The user can also list edit and remove projects.
The user can also quit and save the current projects list (with tasks) to a json-file, and then restart the application with the content of this file restored. The 
interface looks similar to the mockup below:

![TodoList-meny](https://github.com/user-attachments/assets/2c4553ad-b36a-4151-9043-ab28bfb93d27)

Requirements

The solution achieves the following requirements:

* Model projects with a name.
* Model tasks with a title, description, due date, status andm what project they belong to.
* Display a collection of tasks that can be sorted both by date and project
* Support the ability to add, edit, and remove projects.
* Support the ability to add, edit (including mark as done), and remove tasks.
* Support a text-based user interface
* Load and save task list to a file.
