$(function () {
    $("#autorDiv").validate({
        rules: {
            AuthorNameTextBox: {
                required: true,
                regularExpression: "^[a-zA-Z]+"
            },
            AuthorSurnameTextBox: {
                required: true,
                regularExpression: "^[a-zA-Z]+"
            }

        },
        messages: {
            AuthorNameTextBox: {
                required: "wymagane",
                regularExpression: "zly format"
            },
            AuthorSurnameTextBox: {
                required: "wymagane",
                regularExpression: "zly format"
            }
        }
        
    });
});