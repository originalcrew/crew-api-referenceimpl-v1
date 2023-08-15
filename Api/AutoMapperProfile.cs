using AutoMapper;
using Crew.Api.ReferenceImpl.V1.Messages;
using Crew.Api.ReferenceImpl.V1.Models;

namespace Crew.Api.ReferenceImpl.V1
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetExampleRequest, GetExampleQuery>();
            CreateMap<Messages.GetExampleResponse, Models.GetExampleResponse>();
            CreateMap<Messages.GetExampleResponse.AddressObj, Models.GetExampleResponse.AddressObj>();

            CreateMap<GetExamplesRequest, FindExamplesQuery>();
            CreateMap<FindExamplesResponse, GetExamplesResponse>();
            CreateMap<FindExamplesResponse.Example, GetExamplesResponse.Example>();

            CreateMap<PostExampleRequest, CreateExampleCommand>();
            CreateMap<PostExampleRequest.AddressObj, CreateExampleCommand.AddressObj>();
            CreateMap<CreateExampleResponse, PostExampleResponse>();

            CreateMap<PutExamplePersonalDetailsRequest, UpdateExamplePersonalDetailsCommand>();

            CreateMap<PutExampleContactDetailsRequest, UpdateExampleContactDetailsCommand>();
            CreateMap<PutExampleContactDetailsRequest.AddressObj, UpdateExampleContactDetailsCommand.AddressObj>();

            CreateMap<PutExampleApprovalStatusRequest, UpdateExampleApprovalStatusCommand>();

            CreateMap<DeleteExampleRequest, DeleteExampleCommand>();
        }
    }
}