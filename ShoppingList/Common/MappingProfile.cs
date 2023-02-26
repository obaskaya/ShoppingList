using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.CategoryApplication.Command.CreateCommand;
using ShoppingList.Application.CategoryApplication.Query;
using ShoppingList.Application.CategoryApplication.Query.GetByCreateDate;
using ShoppingList.Application.CategoryApplication.Query.GetById;
using ShoppingList.Application.CategoryApplication.Query.GetByPublishDate;
using ShoppingList.Application.ItemApplication.Command.CreateCommand;
using ShoppingList.Application.ItemApplication.Query;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.Application.ListApplication.Query.GetListById;
using ShoppingList.Application.ListApplication.Query.GetListByName;
using ShoppingList.DbOperations;
using ShoppingList.Entities;

namespace ShoppingList.Common
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //List Mapping
            CreateMap<int, List>().ForMember(c => c.Id, c => c.MapFrom(c => c));

            //Create list Mapping
            CreateMap<CreateListModel, List>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(id => new Category { Id = id })))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(id => new Item { Id = id })))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed));

            //Adding Categories and Items to the List
            CreateMap<List, ListsViewModel>()
                .ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories.Select(c => c.Name + " Created Date: " + 
                c.CreatedDate.ToString("yyyy-MM-dd") + " Finished Date: " + c.FinishedDate.ToString("yyyy-MM-dd")).ToList()))
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));

            CreateMap<List, ListByIdViewModel>()
                .ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories.Select(c => c.Name + " Created Date: " + 
                c.CreatedDate.ToString("yyyy-MM-dd") + " Finished Date: " + c.FinishedDate.ToString("yyyy-MM-dd")).ToList()))
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));

            CreateMap<List, ListByNameViewModel>()
                .ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories.Select(c => c.Name + " Created Date: " +
                c.CreatedDate.ToString("yyyy-MM-dd") + " Finished Date: " + c.FinishedDate.ToString("yyyy-MM-dd")).ToList()))
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));

            //Category Mapping
            CreateMap<int, Category>().ForMember(c => c.Id, c => c.MapFrom(c => c));

            //Create Category Mapping
            CreateMap<CreateCategoryModel, Category>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(id => new Item { Id = id })));

            // Adding Items to Category View
            CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));
            CreateMap<Category, CategoryByIdViewModel>()
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));
            CreateMap<Category, CreateDateViewModel>()
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));
            CreateMap<Category, FinishDateViewModel>()
                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));


            //Item Mapping
            CreateMap<CreateItemViewModel, Item>();
            CreateMap<Item, ItemByIdViewModel>();
            CreateMap<Item, ItemViewModel>();


        }
    }
}
