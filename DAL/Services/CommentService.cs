using Common.Repositories;
using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
	// DAL
	// public class CommentService
	// BLL
	//public class CommentService : ICommentRepository<Comment>
	// ASP MVC
	public class CommentService : BaseService, ICommentRepository<Comment>
	{
		// ASP MVC + new SqlConnection(_connectionString)
		public CommentService(IConfiguration config) : base(config, "Main-DB") { }

		// DAL & BLL + new SqlConnection(ConnectionString)
		//private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WAD24-DemoASP-DB;Integrated Security=True;";

		public void Delete(Guid comment_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Delete";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(comment_id), comment_id);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public Comment Get(Guid comment_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_GetById";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(comment_id), comment_id);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return reader.ToComment();
						}
						else
						{
							throw new ArgumentOutOfRangeException(nameof(comment_id));
						}
					}
				}
			}
		}

		public IEnumerable<Comment> GetFromUser(Guid user_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_GetByUserId";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(user_id), user_id);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							yield return reader.ToComment();
						}
					}
				}
			}
		}

		public IEnumerable<Comment> GetFromCocktail(Guid cocktail_id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_GetByCocktailId";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(cocktail_id), cocktail_id);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							yield return reader.ToComment();
						}
					}
				}
			}
		}

		public Guid Insert(Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Insert";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);
					command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
					command.Parameters.AddWithValue("cocktail_id", (object?)comment.Concern ?? DBNull.Value);
					command.Parameters.AddWithValue("user_id", (object?)comment.CreatedBy ?? DBNull.Value);
					command.Parameters.AddWithValue(nameof(Comment.Note), comment.Note);
					connection.Open();
					return (Guid)command.ExecuteScalar();
				}
			}
		}

		public void Update(Guid comment_id, Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Update";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(comment_id), comment_id);
					command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);
					command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
					command.Parameters.AddWithValue(nameof(Comment.Note), comment.Note);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}