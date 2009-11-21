/*
 *
 * NDbUnit
 * Copyright (C)2005 - 2009
 * http://code.google.com/p/ndbunit
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System;

namespace NDbUnit.Core.SqlClient
{
    public class SqlDbCommandBuilder : DbCommandBuilder
    {
        public SqlDbCommandBuilder(IDbConnection connection)
            : base(connection)
        {
        }

        public SqlDbCommandBuilder(string connectionString)
            : base(connectionString)
        {
        }

        public override string QuotePrefix
        {
            get { return "["; }
        }

        public override string QuoteSuffix
        {
            get { return "]"; }
        }

        protected override IDbCommand CreateDbCommand()
        {
            SqlCommand command = new SqlCommand();
            if (CommandTimeOutSeconds != 0)
                command.CommandTimeout = CommandTimeOutSeconds;

            return command;
        }

        protected override IDataParameter CreateNewSqlParameter(int index, DataRow dataRow)
        {
            return new SqlParameter("@p" + index, (SqlDbType)dataRow["ProviderType"],
                                    (int)dataRow["ColumnSize"], (string)dataRow["ColumnName"]);
        }

        protected override IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

    }
}
