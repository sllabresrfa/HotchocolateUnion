Uncomments line 25 in CompanyQuery.cs 

query GetCompanies {
  companies {
    companyId
    history {
      ... on HistoryTypeOne      
      {
        discriminator
        __typename        
      } 
      ... on HistoryTypeTwo {
        discriminator
        __typename
      }
    }
  }
}