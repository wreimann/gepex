/*
      Autor: Wellingthon Reimann         Data: 19/07/2010
   Objetivo: Implementação da classe de controle de sessão do NHibernate.
Atualização:
*/
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace Model.Base
{
    public static class SessionHelper  
    {
        public static readonly string KEY = "GEPEX.Session";
        private static ISessionFactory sessionFactory;

        private static ISessionFactory GetSessionFactory()
        {
            if (sessionFactory == null)
            {
                NHibernate.Cfg.Environment.UseReflectionOptimizer = false;
                Configuration config = new Configuration();
                if (config == null)
                    throw new InvalidOperationException("NHibernate não configurado.");
                config.Configure();

                sessionFactory = config.BuildSessionFactory();
                if (sessionFactory == null)
                    throw new InvalidOperationException("Arquivo de configuração do NHibernate não localizado.");
            }
            return sessionFactory;
        }


        public static void OpenSession()
        {
            ISession sessao = GetSession();
            if (sessao == null)
            {
                sessao = GetSessionFactory().OpenSession();
                SetSession(sessao);
            }
        }

        private static ISession GetSession()
        {
            ISession resultado;
            if (HttpContext.Current == null)
            {
                resultado = (ISession)CallContext.GetData(KEY);

            }
            else
                resultado = (ISession)HttpContext.Current.Items[KEY];
            return resultado;
        }

        public static ISession CurrentSession
        {
            get
            {
                ISession resultado = GetSession();
                if (resultado == null)
                    throw new InvalidOperationException("There is no current session.");
                return resultado;
            }
        }

        private static void SetSession(ISession session)
        {
            if (session != null)
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Items[KEY] = session;
                else
                    CallContext.SetData(KEY, session);
            }
            else
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Items.Remove(KEY);
                else
                    CallContext.FreeNamedDataSlot(KEY);
            }
        }

        public static void CloseSession()
        {
            ISession sessao = GetSession();
            SetSession(null);
            if (sessao != null)
                sessao.Close();
        }

        public static void Flush()
        {
            ISession session = GetSession();
            if (session != null)
                session.Flush();
        }
    }
}
