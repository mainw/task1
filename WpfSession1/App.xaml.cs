using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfSession1.Db;

namespace WpfSession1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Db.TradeEntities _context = new Db.TradeEntities();
        public static Db.User _user;
        public static event EventHandler pageUpdateEvent;
        public static Visibility AdminAccess
        {
            get
            {
                if(_user?.Role.RoleName == "Администратор")
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }
        public static void UpdatePage()
        {
            pageUpdateEvent.Invoke(null, EventArgs.Empty);
        }
        public static void ResetChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {

                }
                else
                {
                    entry.Reload();
                }
            }
            UpdatePage();
        }
        public static bool SaveChanges()
        {
            bool responce = false;
            try
            {
                _context.SaveChanges();
                responce = true;
            }
            catch (DbUpdateException updateEx)
            {
                MessageBox.Show(updateEx.Message);
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Added)
                    {

                    }
                    else
                    {
                        entry.Reload();
                    }
                }
                UpdatePage();
            }
            catch
            {
                ShowValidationErrorMessage(_context);
            }
            return responce;
        }
        public static bool SaveChangesAsync()
        {
            bool responce = false;
            try
            {
                _context.SaveChangesAsync();
                responce = true;
            }
            catch (DbUpdateException updateEx)
            {
                MessageBox.Show(updateEx.Message);
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    entry.Reload();
                }
                UpdatePage();
            }
            catch
            {
                ShowValidationErrorMessage(_context);
            }
            return responce;
        }
        static public void ShowValidationErrorMessage(TradeEntities db)
        {
            try
            {
                string responce = "";
                foreach (DbEntityValidationResult entityErrors in db.GetValidationErrors())
                {
                    foreach (DbValidationError error in entityErrors.ValidationErrors)
                    {
                        responce += error + "\n";
                    }
                    entityErrors.ValidationErrors.Clear();
                }
                UpdatePage();
                MessageBox.Show(responce);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex);
            }
        }
    }
}
