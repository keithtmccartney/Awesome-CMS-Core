using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AwesomeCMSCore.Modules.Admin.Repositories;
using AwesomeCMSCore.Modules.Admin.ViewModels;
using AwesomeCMSCore.Modules.Helper.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCMSCore.Modules.Admin.Controllers.API.V1
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiVersion("1.0")]
	[ApiExplorerSettings(GroupName = "v1")]
	[Route("api/v{version:apiVersion}/Settings/")]
	public class SettingsController: Controller
	{
		private readonly ISettingsRepository _settingsRepository;
		public SettingsController(ISettingsRepository settingsRepository)
		{
			_settingsRepository = settingsRepository;
		}

		[HttpGet("Cron")]
		public async Task<IActionResult> GetCronSetting()
		{
			var result = await _settingsRepository.GetCronSetting();
			return Ok(result);
		}
		
		[HttpPost("Cron"), ValidModel]
		public async Task<IActionResult> SaveCronSetting([FromBody]CronSetting cronSetting)
		{
			var result = await _settingsRepository.SaveCronSetting(cronSetting.CronValue);
			if (!result)
			{
				return BadRequest(); 
			}

			return Ok();
		}
	}
}
