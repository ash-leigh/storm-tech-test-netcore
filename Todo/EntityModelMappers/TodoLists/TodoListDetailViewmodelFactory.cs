using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool orderByRank)
        {
            var items = orderByRank ? 
                        todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).OrderBy(x => x.Rank).ToList() :
                        todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).ToList();

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, orderByRank);
        }
    }
}