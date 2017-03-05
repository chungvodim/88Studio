using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.IO;
using System.Linq.Dynamic;
using System.Threading;

namespace _88Studio.Resource
{
    public class DatabaseResourceManager : IDisposable
    {
        private DatabaseResourceContext _Context;
        private DatabaseResourceContext Context
        {
            get
            {
                if (_Context == null)
                {
                    _Context = new DatabaseResourceContext();
                }
                return _Context;
            }
        }
        public static string GetHtmlString(LocalizationResourceGroup? resourceGroup, string missingResourceValue = null, [CallerMemberName] string callerName = null, [CallerFilePath]string callerFile = null)
        {
            var callerFilename = "";
            if (!string.IsNullOrWhiteSpace(callerFile))
            {
                callerFilename = Path.GetFileNameWithoutExtension(callerFile);
            }
            var key = callerFilename + "_" + callerName;
            var rs = GetString(key, LanguageCode.StandardTo2dehands(Thread.CurrentThread.CurrentUICulture.Name), resourceGroup, missingResourceValue);
            rs = "<rs data-key=" + key + ">" + rs + "</rs>";
            return rs;
        }

        public static string GetResource(Type resourceType, string resourceName)
        {
            var property = resourceType.GetProperty(resourceName);

            if (property == null)
            {
                throw new Exception(string.Format("Resource name \"{0}\" is not found in type \"{1}\"", resourceName, resourceType.FullName));
            }

            return property.GetValue(null).ToString();
        }

        public static string GetString(LocalizationResourceGroup? resourceGroup, string missingResourceValue = null, [CallerMemberName] string callerName = null, [CallerFilePath]string callerFile = null)
        {
            var callerFilename = "";
            if (!string.IsNullOrWhiteSpace(callerFile))
            {
                callerFilename = Path.GetFileNameWithoutExtension(callerFile);
            }
            var key = callerFilename + "_" + callerName;
            var rs = GetString(key, LanguageCode.StandardTo2dehands(Thread.CurrentThread.CurrentUICulture.Name), resourceGroup, missingResourceValue);
            return rs;
        }

        public static string GetString(string resourceKey, string languageCode, LocalizationResourceGroup? resourceGroup, string missingResourceValue = null)
        {
            if (languageCode == null)
            {
                languageCode = LanguageCode.StandardTo2dehands(Thread.CurrentThread.CurrentUICulture.Name);
            }

            using (var db = new DatabaseResourceContext())
            {
                Resource resource = null;
                try
                {
                    resource = db.LocalizationResources.SingleOrDefault(x => x.Key == resourceKey && x.LanguageCode.ToLower() == languageCode.ToLower());
                }
                catch (Exception)
                {
                    // Temporary fix for first DB creation, multithread issue
                    // TODO: cache localization?
                    return missingResourceValue;
                }

                if (resource == null)
                {
                    if (missingResourceValue == null)
                    {
                        throw new Exception(string.Format("Resource with key \"{0}\", language \"{1}\" is not found", resourceKey, languageCode));
                    }
                    else
                    {
                        // add the missing resource to database
                        db.LocalizationResources.Add(new Resource() { LanguageCode = languageCode, Key = resourceKey, Group = resourceGroup, Value = missingResourceValue });

                        db.SaveChanges();

                        return missingResourceValue;
                    }
                }

                return resource.Value;
            }
        }

        public void Update(ResourceModel model)
        {
            var resource = Context.LocalizationResources.SingleOrDefault(x => x.Key == model.Key && x.LanguageCode.ToLower() == model.LanguageCode.ToLower());

            if (resource == null)
            {
                if (string.IsNullOrWhiteSpace(model.Value))
                {
                    throw new Exception(string.Format("Resource with key \"{0}\", language \"{1}\" is not found", model.Key, model.LanguageCode));
                }
                else
                {
                    // add the missing resource to database
                    Context.LocalizationResources.Add(new Resource() { LanguageCode = model.LanguageCode, Key = model.Key, Value = model.Value });
                    Context.SaveChanges();
                }
            }
            else
            {
                var entry = Context.Entry(resource);
                entry.Property(x => x.Value).CurrentValue = model.Value;
                Context.SaveChanges();
            }
        }

        public IEnumerable<ResourceModel> GetByKey(string key)
        {
            var resources = Context.LocalizationResources.Where(x => x.Key == key).ToList();

            if (resources != null && resources.Count > 0)
            {
                return resources.Select(x => new ResourceModel { LanguageCode = x.LanguageCode, Key = key, Value = x.Value });
            }
            else
            {
                return null;
            }
        }

        public IQueryable<ResourceModel> QueryAll()
        {
            return Context.LocalizationResources.Select(x => new ResourceModel { ID = x.LocalizationResourceID, LanguageCode = x.LanguageCode, Key = x.Key, Value = x.Value }).AsQueryable();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
