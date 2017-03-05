using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PagedList;
using System.Linq.Dynamic;
using System.Text;
using System.Reflection;
using _88Studio.Utils.Common;
using _88Studio.Dto;
using System.Web.Mvc;
using _88Studio.Enum;

namespace _88Studio.Web.Models
{
    public class DefaultPagingParams
    {
        public const int DefaultPageSize = 10;
        public const string DefaultSortField = "ID";
    }
    public class PagingParams
    {
        public PagingParams()
        {
            SortField = DefaultPagingParams.DefaultSortField;
            IsAscending = false;
            PageSize = DefaultPagingParams.DefaultPageSize;
            CurrentPage = 0;
        }

        public bool IsAscending { get; set; }
        public string SortField { get; set; }
        private int _CurrentPage { get; set; }
        public int CurrentPage
        {
            get
            {
                if (_CurrentPage < 1)
                {
                    this._CurrentPage = 1;
                }
                return this._CurrentPage;
            }
            set
            {
                this._CurrentPage = value;
            }
        }
        public string CssClass { get; private set; }
        public int PageSize { get; set; }
        public string ExportType { get; set; }

        #region Filter

        public string SearchString { get; set; }
        public string ListingStatus { get; set; }
        public CompanyStatus? CompanyStatus { get; set; }
        public BranchStatus? BranchStatus { get; set; }
        public UserStatus? UserStatus { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyID { get; set; }
        public string BranchName { get; set; }
        public int? BranchID { get; set; }
        public string AdsNo { get; set; }
        public int AdID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion

        private PagingParams Clone()
        {
            var clone = new PagingParams();
            foreach (var propInfo in clone.GetType().GetProperties())
            {
                if (propInfo.CanWrite)
                {
                    var value = propInfo.GetValue(this);
                    propInfo.SetValue(clone, value);
                }
            }
            return clone;
        }

        public PagingParams UpdatePagingParams<T>(PagingParams<T> pp) where T : BaseDto
        {
            if(pp != null)
            {
                foreach (var propInfo in pp.BasicParams.GetType().GetProperties())
                {
                    if (propInfo.CanWrite)
                    {
                        var value = propInfo.GetValue(pp.BasicParams);
                        propInfo.SetValue(this, value);
                    }
                }
            }
            return this;
        }

        public PagingParams SetExportType(string type)
        {
            var pagingParams = this.Clone();
            pagingParams.ExportType = type;
            return pagingParams;
        }

        public PagingParams UpdateAdsNo(string adsNo)
        {
            var pagingParams = this.Clone();
            pagingParams.AdsNo = adsNo;
            return pagingParams;
        }

        public PagingParams UpdateCurrentPage(int page)
        {
            this.CurrentPage = page;
            return this;
        }

        public PagingParams UpdatePageSize(int pageSize)
        {
            var pagingParams = this.Clone();
            pagingParams.PageSize = pageSize;
            return pagingParams;
        }

        public PagingParams UpdateSortField(string sortField)
        {
            var pagingParams = this.Clone();
            pagingParams.SortField = sortField;
            pagingParams.IsAscending = !pagingParams.IsAscending;
            return pagingParams;
        }

        public PagingParams UpdateSortField(string sortField, string currentSortField)
        {
            var pagingParams = this.Clone();
            pagingParams.SortField = sortField;
            UpdateSortingCssClass(pagingParams.SortField, currentSortField);
            pagingParams.IsAscending = !pagingParams.IsAscending;
            return pagingParams;
        }

        public PagingParams UpdateSortField<TModel>(Expression<Func<TModel, object>> expression, bool? isAscending = null) where TModel : BaseDto
        {
            var pagingParams = this.Clone();
            pagingParams.SortField = GetMemberName(expression.Body);
            pagingParams.IsAscending = !pagingParams.IsAscending;
            if (isAscending != null)
            {
                pagingParams.IsAscending = isAscending.Value;
            }
            else
            {
                pagingParams.IsAscending = !pagingParams.IsAscending;
            }
            return pagingParams;
        }

        public PagingParams UpdateSortField<TModel>(Expression<Func<TModel, object>> expression, string currentSortField, bool? isAscending = null) where TModel : BaseDto
        {
            var pagingParams = this.Clone();
            pagingParams.SortField = GetMemberName(expression.Body);
            UpdateSortingCssClass(pagingParams.SortField, currentSortField);
            if(isAscending != null)
            {
                pagingParams.IsAscending = isAscending.Value;
            }
            else
            {
                pagingParams.IsAscending = !pagingParams.IsAscending;
            }
            return pagingParams;
        }

        private void UpdateSortingCssClass(string sortField, string currentSortField)
        {
            if (currentSortField != sortField)
            {
                this.CssClass = "sorting";
            }
            else
            {
                this.CssClass = this.IsAscending ? "sorting_asc" : "sorting_desc";
            }
        }

        public PagingParams UpdateOrder(bool isAscending)
        {
            var pagingparams = this.Clone();
            pagingparams.IsAscending = isAscending;
            return pagingparams;
        }

        public PagingParams UpdateSearchString(string searchString)
        {
            var pagingparams = this.Clone();
            pagingparams.SearchString = searchString;
            return pagingparams;
        }

        public PagingParams UpdateFilter(string name, string value)
        {
            var pagingParams = this.Clone();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                pagingParams.SetProperty(name, value);
            }
            return pagingParams;
        }

        public PagingParams UpdateFilter<TModel>(Expression<Func<TModel, bool>> expression)
        {
             var pagingParams = this.Clone();
            if (expression.Body is BinaryExpression)
            {
                var propertyName = "";

                var binaryExpression = expression.Body as BinaryExpression;
                var left = binaryExpression.Left;
                if(left != null)
                {
                    propertyName = GetMemberName(left);
                }

                var right = binaryExpression.Right;
                if (right != null)
                {
                    var rightValueDelegate = Expression.Lambda(right).Compile();
                    if (rightValueDelegate != null)
                    {
                        var rightValue = rightValueDelegate.DynamicInvoke();
                        if(rightValue != null && !string.IsNullOrEmpty(propertyName))
                        {
                            pagingParams.SetProperty(propertyName, rightValue);
                        }
                    }
                }
            }
            return pagingParams;
        }

        private string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("Expression can not be null");
            }
            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }
            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }
            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }
            throw new ArgumentException("Invalid Expression");
        }

        private string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }
            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
    }

    public class PagingParams<T> : BaseDto where T : BaseDto
    {
        public PagingParams()
        {
            this.BasicParams = new PagingParams();
        }

        public PagingParams BasicParams { get; set; }
        public Expression<Func<T, object>> SortBy { get; set; }
        //public Expression<Func<T, bool>> Predicate { get; set; }
        public IQueryable<T> Query { get; set; }
        public PagedList.IPagedList<T> Items
        {
            get;
            private set;
        }
        public void UpdatePagingParams(PagingParams pagingParams)
        {
            if (pagingParams != null)
            {
                this.BasicParams = pagingParams;
            }
        }
        public PagingParams<T> ToPagedList()
        {
            BuildPagingQuery();
            if(this.Query != null)
            {
                this.Items = Query.ToPagedList(this.BasicParams.CurrentPage, this.BasicParams.PageSize);
            }
            return this;
        }

        public List<T> ToFullList()
        {
            BuildPagingQuery();
            if (this.Query != null)
            {
                return Query.ToList();
            }
            return null;
        }

        private void BuildPagingQuery()
        {
            if (this.Query != null)
            {
                if (!string.IsNullOrWhiteSpace(this.BasicParams.SearchString))
                {
                    string[] keys = this.BasicParams.SearchString.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var total = this.Query.ToList();
                    foreach (string key in keys)
                    {
                        total = total.Where(x => x.ToSearchString().Contains(key.ToLower())).ToList();
                    }
                    this.Query = total.AsQueryable();
                }

                // TODO:

                //if (!string.IsNullOrWhiteSpace(this.BasicParams.FilterKey) && this.BasicParams.FilterValue != null)
                //{
                //    try
                //    {
                //        var predicate = Utils.Common.PredicateBuilder.CreateLambdaExpression<T>(this.BasicParams.FilterKey, this.BasicParams.FilterValue);
                //        Query = Query.Where(predicate);
                //    }
                //    catch (Exception ex)
                //    {
                //        ErrorHelper.Logger.Error(ex);
                //    }
                //}

                if (SortBy != null)
                {
                    this.Query = this.Query.OrderBy(SortBy);
                }
                else
                {
                    this.Query = this.Query.OrderBy(this.BasicParams.SortField + (this.BasicParams.IsAscending ? " ASC" : " DESC"));
                }
            }
        }

    }
}