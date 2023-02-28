using DssHRI.Domain.MRM;
using MediatR;

namespace DssHRI.Application.Queries
{
    public record GenerateSASUrlQuery():IRequest<SASUrlResponse>;
 

}
