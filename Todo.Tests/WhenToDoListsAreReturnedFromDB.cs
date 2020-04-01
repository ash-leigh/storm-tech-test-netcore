using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Xunit;

namespace Todo.Tests
{
    public class WhenToDoListsAreReturnedFromDB
    {
        private readonly List<TodoList> srcTodoLists;
        private readonly TodoListIndexViewmodel result;

        public WhenToDoListsAreReturnedFromDB()
        {
            srcTodoLists = new List<TodoList>();
            srcTodoLists.Add(new TestTodoListBuilder(new IdentityUser("alice@example.com"), "shopping")
                    .WithItem("bread", Importance.High, "alice@example.com")
                    .Build())
                ;

            srcTodoLists.Add(new TestTodoListBuilder(new IdentityUser("bob@example.com"), "shopping")
                    .WithItem("bread", Importance.High, "alice@example.com")
                    .Build())
                ;

            result = TodoListIndexViewmodelFactory.Create(srcTodoLists);
        }

        [Fact]
        public void TestIndexIncludesListsWhereUserIsResponsibleForItems()
        {
            Assert.Equal(srcTodoLists.Count(), result.Lists.Count());
        }
    }
}
