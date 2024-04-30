using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Specifications;

public class PagingSpecification<TEntity> : Specification<TEntity> where TEntity : class, IEntity {
    
    public PagingSpecification(int pageSize, int pageNumber) : base(null) {

        PagingParameters = new PagingParameters(pageSize, pageNumber);
    }
}