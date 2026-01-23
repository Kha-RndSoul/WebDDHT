using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebDDHT.Repositories.Interfaces
{
    /// <summary>
    /// Generic Repository Interface - CRUD operations cơ bản
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        // ========== READ OPERATIONS ==========

        /// <summary>
        /// Lấy tất cả records
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy entity theo ID
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Tìm entities theo điều kiện (predicate)
        /// </summary>
        /// <param name="predicate">Lambda expression:  p => p.IsActive == true</param>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Lấy entity đầu tiên thỏa điều kiện
        /// </summary>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Đếm tổng số records
        /// </summary>
        int Count();

        /// <summary>
        /// Đếm số records thỏa điều kiện
        /// </summary>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Kiểm tra có tồn tại record nào thỏa điều kiện không
        /// </summary>
        bool Any(Expression<Func<T, bool>> predicate);

        // ========== WRITE OPERATIONS ==========

        /// <summary>
        /// Thêm entity mới (chưa SaveChanges)
        /// </summary>
        void Add(T entity);

        /// <summary>
        /// Thêm nhiều entities (chưa SaveChanges)
        /// </summary>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Update entity (chưa SaveChanges)
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Xóa entity (chưa SaveChanges)
        /// </summary>
        void Remove(T entity);

        /// <summary>
        /// Xóa nhiều entities (chưa SaveChanges)
        /// </summary>
        void RemoveRange(IEnumerable<T> entities);

        // ========== ADVANCED OPERATIONS ==========

        /// <summary>
        /// Lấy với eager loading (Include related entities)
        /// Usage: GetWithIncludes(p => p.Category, p => p.Brand)
        /// </summary>
        IEnumerable<T> GetWithIncludes(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Lấy với pagination
        /// </summary>
        /// <param name="page">Số trang (bắt đầu từ 1)</param>
        /// <param name="pageSize">Số records mỗi trang</param>
        /// <param name="totalRecords">Output:  Tổng số records</param>
        IEnumerable<T> GetPaged(int page, int pageSize, out int totalRecords);

        /// <summary>
        /// Lấy với sorting
        /// </summary>
        /// <param name="orderBy">Lambda:  p => p.CreatedAt</param>
        /// <param name="ascending">true = ASC, false = DESC</param>
        IEnumerable<T> GetOrdered<TKey>(Expression<Func<T, TKey>> orderBy, bool ascending = true);
    }
}