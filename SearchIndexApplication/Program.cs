using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using SearchIndexApplication;

namespace SearchIndexApp
{
	class Program
	{

		static void Main(string[] args)
		{
			string serviceName = "boote";
			string apiKey = "";
			string indexName = "product";

			// Create a SearchIndexClient to send create/delete index commands
			Uri serviceEndpoint = new Uri($"https://{serviceName}.search.windows.net/");
			AzureKeyCredential credential = new AzureKeyCredential(apiKey);
			SearchIndexClient adminClient = new SearchIndexClient(serviceEndpoint, credential);

			// Create a SearchClient to load and query documents
			SearchClient srchclient = new SearchClient(serviceEndpoint, indexName, credential);


			//RunQueries(srchclient);
			//QueryPrice(srchclient);
			//QuerySize(srchclient);
			//QueryBrand(srchclient);
			//SearchGenerci(srchclient);


			SearchOptions options;
			SearchResults<Item> response;

			//Console.WriteLine("mostrami i caschi disponibili");
			//query = Console.ReadLine();

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				Size = 5,
				QueryType = Azure.Search.Documents.Models.SearchQueryType.Semantic,
				SemanticSearch = new()
				{
					SemanticConfigurationName = "title",
					QueryCaption = new(QueryCaptionType.Extractive)
				}
				
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("title");
			options.Select.Add("description");

			response = srchclient.Search<Item>("quali caschi avete", options);
			WriteDocuments(response);

		}

		// Write search results to console
		private static void WriteDocuments(SearchResults<Item> searchResults)
		{
			var count = searchResults.TotalCount;
			foreach (SearchResult<Item> result in searchResults.GetResults())
			{
				Console.WriteLine(result.Document);
			}

			Console.WriteLine();
		}

		private static void WriteDocuments(AutocompleteResults autoResults)
		{
			foreach (AutocompleteItem result in autoResults.Results)
			{
				Console.WriteLine(result.Text);
			}

			Console.WriteLine();
		}

		// Run queries, use WriteDocuments to print output
		private static void RunQueries(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			// Query 1
			Console.WriteLine("Query #1: Search on empty term '*' to return all documents, showing a subset of fields...\n");

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				Filter = "",
				OrderBy = { "" }
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("price");
			options.Select.Add("brand");
			options.Select.Add("size");
			options.Select.Add("title");
			options.Select.Add("description");
			options.Select.Add("availability");

			response = srchclient.Search<Item>("*", options);
			WriteDocuments(response);
		}

		private static void QueryPrice(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			// Query 2
			Console.WriteLine("Query #2: Search on 'items', sort by Price in descending order...\n");

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				Filter = "price gt 50",
				OrderBy = { "price desc" }
			};

			options.IncludeTotalCount = true;

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("price");
			options.Select.Add("brand");
			options.Select.Add("size");
			options.Select.Add("title");
			options.Select.Add("description");
			options.Select.Add("availability");

			response = srchclient.Search<Item>(options);
			WriteDocuments(response);
		}

		private static void QuerySize(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			Console.WriteLine("Query #3: Limit search to specific fields ...\n");

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				SearchFields = { "size" }
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("price");
			options.Select.Add("brand");
			options.Select.Add("size");
			options.Select.Add("title");
			options.Select.Add("description");
			options.Select.Add("availability");

			response = srchclient.Search<Item>("TU", options);
			WriteDocuments(response);
		}

		private static void QueryBrand(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			Console.WriteLine("Query #3: Limit search to specific fields ...\n");

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				SearchFields = { "brand" }
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("price");
			options.Select.Add("brand");
			options.Select.Add("size");
			options.Select.Add("title");
			options.Select.Add("description");
			options.Select.Add("availability");

			response = srchclient.Search<Item>("Bcr", options);
			WriteDocuments(response);
		}

		private static void SearchGenerci(SearchClient srchclient)
		{
			SearchOptions options;
			SearchResults<Item> response;

			//Console.WriteLine("mostrami i caschi disponibili");
			//query = Console.ReadLine();

			options = new SearchOptions()
			{
				IncludeTotalCount = true,
				QueryType = Azure.Search.Documents.Models.SearchQueryType.Semantic,
				SemanticSearch = new()
				{
					SemanticConfigurationName = "title",
					QueryCaption = new(QueryCaptionType.Extractive)
				}
			};

			options.Select.Add("id");
			options.Select.Add("link");
			options.Select.Add("title");
			options.Select.Add("description");

			response = srchclient.Search<Item>("mostrami l'olio disponibile", options);
			WriteDocuments(response);
		}
	}
}

