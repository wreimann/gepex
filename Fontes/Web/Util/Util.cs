
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
namespace Web.Util
{
    public class Util
    {
        public void Alerta(string msg, System.Web.UI.Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "<script language=JavaScript>alert('"+msg+"')</script>;", false);
        }

        public void RedimensionarImagem(string arquivo, string caminho, int Largura, int Altura)
        {
            try
            {
                Image img = Image.FromFile(caminho + arquivo);
                int oWidth = img.Width; // largura original 
                int oHeight = img.Height; // altura original 
                int vLargura, vAltura;

                if ((oWidth / Largura) > (oHeight / Altura))
                {
                    vLargura = Largura;
                    vAltura = (int)(oHeight * ((float)Largura / (float)oWidth));

                    if (vAltura > Altura)
                    {
                        vLargura = (int)((Largura / Altura) * vLargura);
                        vAltura = Altura;
                    }
                }
                else
                {
                    vAltura = Altura;
                    vLargura = (int)(oWidth * ((float)Altura / (float)oHeight));

                    if (vLargura > Largura)
                    {
                        vAltura = (Altura * (Largura / Altura));
                        vLargura = Largura;
                    }
                }
                
                // cria a copia da imagem 
                Image imgThumb = img.GetThumbnailImage(vLargura, vAltura, null, new System.IntPtr(0));            
                //temp = arquivo + ".tmp";
                imgThumb.Save(caminho + "/tmp/" + arquivo, ImageFormat.Jpeg);
                img.Dispose();
                imgThumb.Dispose();
                //File.Delete(arquivo); // deleta arquivo original 
                //File.Copy(temp, arquivo); // copia a nova imagem 
               // File.Delete(temp); // deleta temporário 

                    
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
