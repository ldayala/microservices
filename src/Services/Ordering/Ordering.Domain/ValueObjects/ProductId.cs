﻿namespace Ordering.Domain
{
    public record ProductId(Guid Value) { 
    
        public static ProductId New() => new(Guid.NewGuid());
    }
   
}