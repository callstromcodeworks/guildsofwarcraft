/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CCW.GoW.DataService;

public class DataHandler
{
    internal readonly string connectionString;
    public DataHandler(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<bool> AddDiscordServer(ServerConfig config)
    {
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add(DataContracts.SpParams.AddDiscordServer.Id, config.Id);
        parameters.Add(DataContracts.SpParams.AddDiscordServer.Name, config.Name);
        parameters.Add(DataContracts.SpParams.AddDiscordServer.Prefix, '!');
        try
        {
            return await con.ExecuteAsync(DataContracts.SpParams.AddDiscordServer._SpName, param: parameters, commandType: CommandType.StoredProcedure) is not 0;
        }
        catch (Exception ex)
        {

            return false;
        }
    }

    public async Task<bool> RemoveDiscordServer(ServerConfig config)
    {
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add(DataContracts.SpParams.RemoveDiscordServer.Id, config.Id);
        if (config.WowId != string.Empty) parameters.Add(DataContracts.SpParams.RemoveDiscordServer.WowId, config.WowId);
        else parameters.Add(DataContracts.SpParams.RemoveDiscordServer.WowId, "-1");
        return await con.ExecuteAsync(DataContracts.SpParams.RemoveDiscordServer._SpName, param: parameters, commandType: CommandType.StoredProcedure) is not 0;
    }

    public async Task<bool> AssociateGuild(ServerConfig config)
    {
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add(DataContracts.SpParams.AssociateGuild.Id, config.Id);
        parameters.Add(DataContracts.SpParams.AssociateGuild.WowId, config.WowId);
        parameters.Add(DataContracts.SpParams.AssociateGuild.Name, config.GuildName);
        parameters.Add(DataContracts.SpParams.AssociateGuild.Faction, config.Faction);
        parameters.Add(DataContracts.SpParams.AssociateGuild.AchievementPoints, config.AchievementPoints);
        parameters.Add(DataContracts.SpParams.AssociateGuild.MemberCount, config.MemberCount);
        parameters.Add(DataContracts.SpParams.AssociateGuild.Realm, config.Realm);
        parameters.Add(DataContracts.SpParams.AssociateGuild.CreationTimestamp, config.CreationTimestamp);
        return await con.ExecuteAsync(DataContracts.SpParams.AssociateGuild._SpName, param: parameters, commandType: CommandType.StoredProcedure) != 0;
    }

    public async Task<bool> DisassociateGuild(ServerConfig config)
    {
        if (config.WowId == string.Empty) return false;
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add(DataContracts.SpParams.DisassociateGuild.Id, config.Id);
        parameters.Add(DataContracts.SpParams.DisassociateGuild.WowId, config.WowId);
        return await con.ExecuteAsync(DataContracts.SpParams.DisassociateGuild._SpName, param: parameters, commandType: CommandType.StoredProcedure) != 0;
    }
    public async Task<bool> UpdateDiscordServer(ServerConfig config)
    {
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add(DataContracts.SpParams.UpdateDiscordServer.Id, config.Id);
        parameters.Add(DataContracts.SpParams.UpdateDiscordServer.Name, config.Name);
        parameters.Add(DataContracts.SpParams.UpdateDiscordServer.Prefix, config.CommandPrefix);
        parameters.Add(DataContracts.SpParams.UpdateDiscordServer.Unavailable, config.Unavailable);
        bool result = false;
        try
        {
            result = await con.ExecuteAsync(DataContracts.SpParams.UpdateDiscordServer._SpName, param: parameters, commandType: CommandType.StoredProcedure) != 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return result;
    }
    public async Task<ServerConfig> LoadServerInfo(string id)
    {
        using SqlConnection con = new(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("id", id);
        ServerConfig result;
        try
        {
            result = await con.QueryFirstAsync<ServerConfig>(sql: DataContracts.SpParams.GetServerInfo._SpNameSingle, param: parameters, commandType: CommandType.StoredProcedure);
        }
        catch (InvalidOperationException ex)
        {
            if (!ex.Message.Contains("no elements")) throw ex;
            result = new(true);
        }
        return result;
    }

    public async Task<HashSet<ServerConfig>> LoadAllServers()
    {
        using SqlConnection con = new(connectionString);
        return (await con.QueryAsync<ServerConfig>(sql: DataContracts.SpParams.GetServerInfo._SpNameAll, commandType: CommandType.StoredProcedure)).ToHashSet();
    }
}
