using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayer.SqlServer
{
    public class OrderDetailDAL : IOrderDetailDAL
    {
        private string connectionString;

        public OrderDetailDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Add(OrderDetail data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int[] orderDetailIDs)
        {
            throw new NotImplementedException();
        }

        public OrderDetail Get(int orderDetailID)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy thông tin chi tiết đơn đặt hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<OrderDetail> List(int orderID)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select * from OrderDetails WHERE OrderID = @OrderID order by ProductID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new OrderDetail()
                            {
                                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                UnitPrice = Convert.ToDecimal(dbReader["UnitPrice"]),
                                Quantity = Convert.ToInt16(dbReader["Quantity"]),
                                Discount = Convert.ToSingle(dbReader["Discount"]),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(OrderDetail data)
        {
            throw new NotImplementedException();
        }
    }
}