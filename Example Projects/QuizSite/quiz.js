$(document).ready(function()
{
	var MaxQuestions = 10;
	var QuizWrapper = $("#QuizWrapper");
	var AddButton = $("#AddMore");
	
	var x = QuizWrapper.length;
	var QuestionCount = 1;
	
	$(AddButton).click(function(e)
	{
		if(x <= MaxQuestions)
		{
			QuestionCount++;
			$(QuizWrapper).append('shit goes here');
			x++;
		}
	return false;
});

$("body").on("click",".removeclass", function(e)
{
	if(x > 1)
	{
		$(this).parent('div').remove();
		x--;
	}
return false;
})

});