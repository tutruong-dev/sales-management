using SV21T1020793.DataLayers;
using SV21T1020793.DomainModels;

namespace SV21T1020793.BusinessLayers
{
    public static class CommonDataService
    {
        private static readonly ISimpleQueryDAL<Province> provinceDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        /// <summary>
        /// 
        /// </summary>
        static CommonDataService()
        {
            string connectionString = @"server=ADMINISTRATOR\MSSQLSERVER2022;user id=sa;password=123456;database=LiteDB;TrustServerCertificate=true";

            provinceDB = new DataLayers.SQLServer.ProvinceDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
        }
        /// <summary>
        /// Danh sách tất cả các tỉnh thành
        /// </summary>
        /// <returns></returns>
        public static List<Province> ListOfProvinces() 
        {
            return provinceDB.List();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên khách hàng hoặc tên giao dịch cần tìm</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer ? GetCustomer(int id)
        { 
            return customerDB.Get(id); 
        }
        /// <summary>
        /// Bổ sung 1 khách hàng mới. Hàm trả về mã của khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin của 1 khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int id) 
        {
            if (customerDB.InUsed(id))
                return false;
            return customerDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem một khách hàng hiện đang có đơn hàng hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int id)
        {
            return customerDB.InUsed(id);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên nhân viên cần tìm</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue);
        }
        public static Employee? GetEmployee(int id)
        {
            return employeeDB.Get(id);
        }

        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        public static bool DeleteEmployee(int id)
        {
            if (employeeDB.InUsed(id))
                return false;
            return employeeDB.Delete(id);
        }

        public static bool InUsedEmployee(int id)
        {
            return employeeDB.InUsed(id);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên người giao hàng cần tìm</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue);
        }
        public static Shipper? GetShipper(int id)
        {
            return shipperDB.Get(id);
        }

        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        public static bool DeleteShipper(int id)
        {
            if (shipperDB.InUsed(id))
                return false;
            return shipperDB.Delete(id);
        }

        public static bool InUsedShipper(int id)
        {
            return shipperDB.InUsed(id);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên loại hàng cần tìm</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue);
        }
        public static Category? GetCategory(int id)
        {
            return categoryDB.Get(id);
        }

        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        public static bool DeleteCategory(int id)
        {
            if (categoryDB.InUsed(id))
                return false;
            return categoryDB.Delete(id);
        }

        public static bool InUsedCategory(int id)
        {
            return categoryDB.InUsed(id);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên nhà cung cấp cần tìm</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue);
        }
        public static Supplier? GetSupplier(int id)
        {
            return supplierDB.Get(id);
        }

        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        public static bool DeleteSupplier(int id)
        {
            if (supplierDB.InUsed(id))
                return false;
            return supplierDB.Delete(id);
        }

        public static bool InUsedSupplier(int id)
        {
            return supplierDB.InUsed(id);
        }

    }
}

//Lớp static là gì? Khác với lớp không phải là static chỗ nào?
// Constructor của lớp static có đặc điểm gì? Được gọi khi nào? Có khác gì với constructor của các lớp không phải static?