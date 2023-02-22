using AutoMapper;
using ShoppingList.Application.ListApplication.Command.CreateCommand;
using ShoppingList.Application.ListApplication.Query;
using ShoppingList.Entities;

namespace ShoppingList.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping List
            CreateMap<int, List>().ForMember(c => c.Id, c => c.MapFrom(c => c));

            //Adding Categories and Items to the List
            CreateMap<List, ListsViewModel>().ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories.Select(c => c.Name + " Created Date: " + c.CreatedDate + " Finished Date: " + c.FinishedDate).ToList()))
                                             .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));
            CreateMap<List, ListByIdViewModel>().ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories.Select(c => c.Name + " Created Date: " + c.CreatedDate + " Finished Date: " + c.FinishedDate).ToList()))
                                                .ForMember(c => c.Items, c => c.MapFrom(c => c.Items.Select(c => c.Name + " Quantity: " + c.Quantity).ToList()));
            CreateMap<CreateListModel, List>().ForMember(c => c.Categories, c => c.MapFrom(c => c.Categories))
                                              .ForMember(c => c.Items, c => c.MapFrom(c => c.Items));
        }
    }
}
