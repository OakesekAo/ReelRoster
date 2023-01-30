// if not on Import page, set listener for general search
(document.title.substring(0, 6) != "Import") ? document.getElementById("searchBtn").addEventListener("click", getMovieId)
 : document.getElementById("importBtn").addEventListener("click", getMovieId)


// Bloodhound with Remote
var movies_suggestions = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.whitespace,
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: {
        url: "https://api.themoviedb.org/3/search/movie?api_key="+apiKey+"&query=%QUERY",
        wildcard: "%QUERY",
        transform: function (response) {
            console.log(apiKey);
            return response.results;
        }
    },
    //identify: function(item){
    //    return item.id;
    //}
});

// init Typeahead
$('#movieSearch').typeahead(
{
    // CONFIGURATION OPTIONS
    highlight: true,
    minLength: 2
},
{
    // DATASET OPTIONS
    name: "titles",
    source: movies_suggestions, // suggestion engine (bloodhound) is passed as the source
    display: function(item){
        return item.title;
    },
    limit: 5,
    templates: {
        notFound: '<div>Not Found</div>',
        pending: "<div>Loading...</div>",
        suggestion: function(item){
            let title = item.title;
            return '<div>'+item.title+'<span id="'+title.toLowerCase()+'" hidden>'+item.id+'</div>';
        }
    }
});

/* STYLING Twitter Typeahead */
$(".twitter-typeahead").addClass("form-control").css("padding",0);
$(".tt-hint,.tt-input").addClass("w-100");
$(".tt-hint").css("opacity",.6);

function getMovieId() {
    let formId = "#searchForm", action = "Details";
    if (document.title.substring(0, 6) == "Import") {
        formId = "#importForm";
        action = "Import";
    }

    let movieId = $(document.getElementById($("#movieSearch").val().toLowerCase())).text();

    $(formId).attr("action", `/Movies/${action}/` + movieId);
}