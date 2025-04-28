using Coursery.Application.UseCases.Add;
using CourseryPL.Controllers.CourseryPL.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CourseryPL.Controllers;

[ApiController]
[Route("api/history")]
public class HistoryController : ControllerBase
{
    private readonly GetHistoryRecord _getHistoryRecord;
    private readonly AddHistoryRecord _addHistoryRecord;

    public HistoryController(GetHistoryRecord getHistoryRecord, AddHistoryRecord addHistoryRecord)
    {
        _getHistoryRecord = getHistoryRecord;
        _addHistoryRecord = addHistoryRecord;
    }
    
    
    [HttpGet]
    public async Task<ActionResult> GetHistory()
    {
        var userId = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]).Id;
        
        var history = await _getHistoryRecord.Execute(userId);
        if (history == null)
        {
            return NotFound();
        }
        return Ok(history);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddHistory([FromBody] AddHistoryRecordDto historyRecord)
    {
         var createdHistory = await _addHistoryRecord.Execute(historyRecord);
        if (createdHistory == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetHistory), new { userId = createdHistory.UserId }, createdHistory);
    }

}