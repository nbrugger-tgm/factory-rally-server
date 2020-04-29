using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tgm.Roborally.Server.Authentication
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class GameAuth : Attribute, IAuthorizationFilter 
	{
		private static readonly string _key = RandomString(256,false);

		public GameAuth(Role neededRole)
		{
			_needed_role = neededRole;
		}

		public string AdminKey => _key;
		
		private AuthResult _result = new AuthResult();
		private Role _needed_role;
		/**
		 * When true the player must match `player_id` from path
		 */
		public bool PlayerSelf = false;

		/**
		 * If true the accessed object (Robot) needs to be owned by the player
		 */
		public bool BelongsTo = false;

		public AuthResult Result => _result;

		public Role Role
		{
			set => _needed_role = value;
		}


		public void OnAuthorization(AuthorizationFilterContext context)
		{
			Console.Out.WriteLine("context = {0}", context);
			bool isAdmin = false;
			bool isPlayer = false;
			if (_needed_role == Role.ADMIN || _needed_role == Role.ANYONE)
			{
				isAdmin = context.HttpContext.Request.Query["skey"] == AdminKey;
				_result = new AuthResult();
				_result.player = -1;
				_result.isAdmin = isAdmin;
			}
			if(!isAdmin)
				if (_needed_role == Role.PLAYER || _needed_role == Role.ANYONE)
				{
					//TODO
					isPlayer = true;
					_result = new AuthResult();
					_result.player = new Random().Next(6);
				}

			if (!isAuthorized(_needed_role,isAdmin,isPlayer))
			{
				context.Result = new UnauthorizedResult();
			}
		}

		private bool isAuthorized(Role neededRole, in bool isAdmin, in bool isPlayer) =>
			(neededRole == Role.ANYONE && (isAdmin || isPlayer)) ||
			(neededRole == Role.PLAYER && isPlayer) ||
			(neededRole == Role.ADMIN && isAdmin);


		private static string RandomString(int size, bool lowerCase)  
		{  
			StringBuilder builder = new StringBuilder();  
			Random random = new Random();  
			char ch;
			for (int i = 0; i < size; i++)  
			{  
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  
				builder.Append(ch);  
			}  
			if (lowerCase)  
				return builder.ToString().ToLower();  
			return builder.ToString();  
		}
	}

	public class AuthResult
	{
		public bool isAdmin = false;
		public int player = -1;
	}
}