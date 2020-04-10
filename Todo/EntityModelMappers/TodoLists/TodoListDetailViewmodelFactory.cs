using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool hideDoneItems)
        {
            var items = hideDoneItems ? todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).Where(x => !x.IsDone).ToList()
                : todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).ToList();
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, hideDoneItems);
        }
    }
}