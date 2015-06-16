//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using LogicalModel.Expressions;
//using LogicalModel.Base;
//using LogicalModel.Validation;
//using Utilities;
//namespace LogicalModel.Validation
//{
//  public class ValidationsX: ValidationFunctionContainer
//  {
//      public ValidationsX()
//      {
//    this.FunctionDictionary.Add("ebav0050h", this.ebav0050h);
//    this.FunctionDictionary.Add("ebav0067h", this.ebav0067h);
//    this.FunctionDictionary.Add("ebav0069h", this.ebav0069h);
//    this.FunctionDictionary.Add("ebav0071h", this.ebav0071h);
//    this.FunctionDictionary.Add("ebav0763m", this.ebav0763m);
//    this.FunctionDictionary.Add("ebav0764m", this.ebav0764m);
//    this.FunctionDictionary.Add("ebav0765m", this.ebav0765m);
//    this.FunctionDictionary.Add("ebav0766m", this.ebav0766m);
//    this.FunctionDictionary.Add("ebav0767m", this.ebav0767m);
//    this.FunctionDictionary.Add("ebav0768m", this.ebav0768m);
//    this.FunctionDictionary.Add("ebav1694m", this.ebav1694m);
//    this.FunctionDictionary.Add("ebav1695m", this.ebav1695m);
//    this.FunctionDictionary.Add("ebav1696m", this.ebav1696m);
//    this.FunctionDictionary.Add("ebav1697m", this.ebav1697m);
//    this.FunctionDictionary.Add("ebav4030m", this.ebav4030m);
//    this.FunctionDictionary.Add("ebav0057h", this.ebav0057h);
//    this.FunctionDictionary.Add("ebav0058h", this.ebav0058h);
//    this.FunctionDictionary.Add("ebav0059h", this.ebav0059h);
//    this.FunctionDictionary.Add("ebav0129h", this.ebav0129h);
//    this.FunctionDictionary.Add("ebav0130h", this.ebav0130h);
//    this.FunctionDictionary.Add("ebav0145h", this.ebav0145h);
//    this.FunctionDictionary.Add("ebav0779m", this.ebav0779m);
//    this.FunctionDictionary.Add("ebav0780m", this.ebav0780m);
//    this.FunctionDictionary.Add("ebav1920h", this.ebav1920h);
//    this.FunctionDictionary.Add("ebav1921h", this.ebav1921h);
//    this.FunctionDictionary.Add("ebav2060s", this.ebav2060s);
//    this.FunctionDictionary.Add("ebav3899s", this.ebav3899s);
//    this.FunctionDictionary.Add("ebav0136h", this.ebav0136h);
//    this.FunctionDictionary.Add("ebav0138h", this.ebav0138h);
//    this.FunctionDictionary.Add("ebav0141h", this.ebav0141h);
//    this.FunctionDictionary.Add("ebav0143h", this.ebav0143h);
//    this.FunctionDictionary.Add("ebav0144h", this.ebav0144h);
//    this.FunctionDictionary.Add("ebav0787m", this.ebav0787m);
//    this.FunctionDictionary.Add("ebav0788m", this.ebav0788m);
//    this.FunctionDictionary.Add("ebav0789m", this.ebav0789m);
//    this.FunctionDictionary.Add("ebav0790m", this.ebav0790m);
//    this.FunctionDictionary.Add("ebav0791m", this.ebav0791m);
//    this.FunctionDictionary.Add("ebav0792m", this.ebav0792m);
//    this.FunctionDictionary.Add("ebav0793m", this.ebav0793m);
//    this.FunctionDictionary.Add("ebav0794m", this.ebav0794m);
//    this.FunctionDictionary.Add("ebav0795m", this.ebav0795m);
//    this.FunctionDictionary.Add("ebav0798m", this.ebav0798m);
//    this.FunctionDictionary.Add("ebav1699m", this.ebav1699m);
//    this.FunctionDictionary.Add("ebav3900s", this.ebav3900s);
//    this.FunctionDictionary.Add("ebav0769m", this.ebav0769m);
//    this.FunctionDictionary.Add("ebav0770m", this.ebav0770m);
//    this.FunctionDictionary.Add("ebav0771m", this.ebav0771m);
//    this.FunctionDictionary.Add("ebav0772m", this.ebav0772m);
//    this.FunctionDictionary.Add("ebav0773m", this.ebav0773m);
//    this.FunctionDictionary.Add("ebav1698m", this.ebav1698m);
//    this.FunctionDictionary.Add("ebav3898s", this.ebav3898s);
//    this.FunctionDictionary.Add("ebav0775m", this.ebav0775m);
//    this.FunctionDictionary.Add("ebav0785m", this.ebav0785m);
//    this.FunctionDictionary.Add("ebav0777m", this.ebav0777m);
//    this.FunctionDictionary.Add("ebav0856m", this.ebav0856m);
//    this.FunctionDictionary.Add("ebav1703m", this.ebav1703m);
//    this.FunctionDictionary.Add("ebav0783m", this.ebav0783m);
//    this.FunctionDictionary.Add("ebav0784m", this.ebav0784m);
//    this.FunctionDictionary.Add("ebav0786m", this.ebav0786m);
//    this.FunctionDictionary.Add("ebav0799m", this.ebav0799m);
//    this.FunctionDictionary.Add("ebav0800m", this.ebav0800m);
//    this.FunctionDictionary.Add("ebav1219m", this.ebav1219m);
//    this.FunctionDictionary.Add("ebav1908h", this.ebav1908h);
//    this.FunctionDictionary.Add("ebav3901s", this.ebav3901s);
//    this.FunctionDictionary.Add("ebav0801m", this.ebav0801m);
//    this.FunctionDictionary.Add("ebav0802m", this.ebav0802m);
//    this.FunctionDictionary.Add("ebav0803m", this.ebav0803m);
//    this.FunctionDictionary.Add("ebav0804m", this.ebav0804m);
//    this.FunctionDictionary.Add("ebav1220m", this.ebav1220m);
//    this.FunctionDictionary.Add("ebav1909h", this.ebav1909h);
//    this.FunctionDictionary.Add("ebav3902s", this.ebav3902s);
//    this.FunctionDictionary.Add("ebav0805m", this.ebav0805m);
//    this.FunctionDictionary.Add("ebav0806m", this.ebav0806m);
//    this.FunctionDictionary.Add("ebav0807m", this.ebav0807m);
//    this.FunctionDictionary.Add("ebav0808m", this.ebav0808m);
//    this.FunctionDictionary.Add("ebav1221m", this.ebav1221m);
//    this.FunctionDictionary.Add("ebav1930h", this.ebav1930h);
//    this.FunctionDictionary.Add("ebav3903s", this.ebav3903s);
//    this.FunctionDictionary.Add("ebav3904s", this.ebav3904s);
//    this.FunctionDictionary.Add("ebav0809m", this.ebav0809m);
//    this.FunctionDictionary.Add("ebav0810m", this.ebav0810m);
//    this.FunctionDictionary.Add("ebav0811m", this.ebav0811m);
//    this.FunctionDictionary.Add("ebav0812m", this.ebav0812m);
//    this.FunctionDictionary.Add("ebav0813m", this.ebav0813m);
//    this.FunctionDictionary.Add("ebav1931h", this.ebav1931h);
//    this.FunctionDictionary.Add("ebav1932h", this.ebav1932h);
//    this.FunctionDictionary.Add("ebav3905s", this.ebav3905s);
//    this.FunctionDictionary.Add("ebav3906s", this.ebav3906s);
//    this.FunctionDictionary.Add("ebav0817m", this.ebav0817m);
//    this.FunctionDictionary.Add("ebav0818m", this.ebav0818m);
//    this.FunctionDictionary.Add("ebav0819m", this.ebav0819m);
//    this.FunctionDictionary.Add("ebav0820m", this.ebav0820m);
//    this.FunctionDictionary.Add("ebav0821m", this.ebav0821m);
//    this.FunctionDictionary.Add("ebav0822m", this.ebav0822m);
//    this.FunctionDictionary.Add("ebav0823m", this.ebav0823m);
//    this.FunctionDictionary.Add("ebav0824m", this.ebav0824m);
//    this.FunctionDictionary.Add("ebav0825m", this.ebav0825m);
//    this.FunctionDictionary.Add("ebav0826m", this.ebav0826m);
//    this.FunctionDictionary.Add("ebav3917s", this.ebav3917s);
//    this.FunctionDictionary.Add("ebav3918s", this.ebav3918s);
//    this.FunctionDictionary.Add("ebav3919s", this.ebav3919s);
//    this.FunctionDictionary.Add("ebav3920s", this.ebav3920s);
//    this.FunctionDictionary.Add("ebav3921s", this.ebav3921s);
//    this.FunctionDictionary.Add("ebav0827m", this.ebav0827m);
//    this.FunctionDictionary.Add("ebav0828m", this.ebav0828m);
//    this.FunctionDictionary.Add("ebav1942h", this.ebav1942h);
//    this.FunctionDictionary.Add("ebav1943h", this.ebav1943h);
//    this.FunctionDictionary.Add("ebav1944h", this.ebav1944h);
//    this.FunctionDictionary.Add("ebav1945h", this.ebav1945h);
//    this.FunctionDictionary.Add("ebav1946h", this.ebav1946h);
//    this.FunctionDictionary.Add("ebav1947h", this.ebav1947h);
//    this.FunctionDictionary.Add("ebav1948h", this.ebav1948h);
//    this.FunctionDictionary.Add("ebav3922s", this.ebav3922s);
//    this.FunctionDictionary.Add("ebav3923s", this.ebav3923s);
//    this.FunctionDictionary.Add("ebav3924s", this.ebav3924s);
//    this.FunctionDictionary.Add("ebav0829m", this.ebav0829m);
//    this.FunctionDictionary.Add("ebav0830m", this.ebav0830m);
//    this.FunctionDictionary.Add("ebav0831m", this.ebav0831m);
//    this.FunctionDictionary.Add("ebav0835m", this.ebav0835m);
//    this.FunctionDictionary.Add("ebav0837m", this.ebav0837m);
//    this.FunctionDictionary.Add("ebav3133m", this.ebav3133m);
//    this.FunctionDictionary.Add("ebav3134m", this.ebav3134m);
//    this.FunctionDictionary.Add("ebav3135m", this.ebav3135m);
//    this.FunctionDictionary.Add("ebav3927s", this.ebav3927s);
//    this.FunctionDictionary.Add("ebav0839m", this.ebav0839m);
//    this.FunctionDictionary.Add("ebav0840m", this.ebav0840m);
//    this.FunctionDictionary.Add("ebav0841m", this.ebav0841m);
//    this.FunctionDictionary.Add("ebav3928s", this.ebav3928s);
//    this.FunctionDictionary.Add("ebav3929s", this.ebav3929s);
//    this.FunctionDictionary.Add("ebav0842m", this.ebav0842m);
//    this.FunctionDictionary.Add("ebav0843m", this.ebav0843m);
//    this.FunctionDictionary.Add("ebav0844m", this.ebav0844m);
//    this.FunctionDictionary.Add("ebav0845m", this.ebav0845m);
//    this.FunctionDictionary.Add("ebav0846m", this.ebav0846m);
//    this.FunctionDictionary.Add("ebav0847m", this.ebav0847m);
//    this.FunctionDictionary.Add("ebav0848m", this.ebav0848m);
//    this.FunctionDictionary.Add("ebav0849m", this.ebav0849m);
//    this.FunctionDictionary.Add("ebav0850m", this.ebav0850m);
//    this.FunctionDictionary.Add("ebav0851m", this.ebav0851m);
//    this.FunctionDictionary.Add("ebav0852m", this.ebav0852m);
//    this.FunctionDictionary.Add("ebav0854m", this.ebav0854m);
//    this.FunctionDictionary.Add("ebav1950h", this.ebav1950h);
//    this.FunctionDictionary.Add("ebav1951h", this.ebav1951h);
//    this.FunctionDictionary.Add("ebav3930s", this.ebav3930s);
//    this.FunctionDictionary.Add("ebav3931s", this.ebav3931s);
//    this.FunctionDictionary.Add("ebav0855m", this.ebav0855m);
//    this.FunctionDictionary.Add("ebav1702m", this.ebav1702m);
//    this.FunctionDictionary.Add("ebav0857m", this.ebav0857m);
//    this.FunctionDictionary.Add("ebav0858m", this.ebav0858m);
//    this.FunctionDictionary.Add("ebav0859m", this.ebav0859m);
//    this.FunctionDictionary.Add("ebav0860m", this.ebav0860m);
//    this.FunctionDictionary.Add("ebav0862m", this.ebav0862m);
//    this.FunctionDictionary.Add("ebav0863m", this.ebav0863m);
//    this.FunctionDictionary.Add("ebav0864m", this.ebav0864m);
//    this.FunctionDictionary.Add("ebav0865m", this.ebav0865m);
//    this.FunctionDictionary.Add("ebav0867m", this.ebav0867m);
//    this.FunctionDictionary.Add("ebav0869m", this.ebav0869m);
//    this.FunctionDictionary.Add("ebav1952h", this.ebav1952h);
//    this.FunctionDictionary.Add("ebav1953h", this.ebav1953h);
//    this.FunctionDictionary.Add("ebav1954h", this.ebav1954h);
//    this.FunctionDictionary.Add("ebav1955h", this.ebav1955h);
//    this.FunctionDictionary.Add("ebav1956h", this.ebav1956h);
//    this.FunctionDictionary.Add("ebav3932s", this.ebav3932s);
//    this.FunctionDictionary.Add("ebav3933s", this.ebav3933s);
//    this.FunctionDictionary.Add("ebav0874m", this.ebav0874m);
//    this.FunctionDictionary.Add("ebav0875m", this.ebav0875m);
//    this.FunctionDictionary.Add("ebav0876m", this.ebav0876m);
//    this.FunctionDictionary.Add("ebav0877m", this.ebav0877m);
//    this.FunctionDictionary.Add("ebav0878m", this.ebav0878m);
//    this.FunctionDictionary.Add("ebav3912s", this.ebav3912s);
//    this.FunctionDictionary.Add("ebav3913s", this.ebav3913s);
//    this.FunctionDictionary.Add("ebav3914s", this.ebav3914s);
//    this.FunctionDictionary.Add("ebav3915s", this.ebav3915s);
//    this.FunctionDictionary.Add("ebav0879m", this.ebav0879m);
//    this.FunctionDictionary.Add("ebav3132m", this.ebav3132m);
//    this.FunctionDictionary.Add("ebav3142m", this.ebav3142m);
//    this.FunctionDictionary.Add("ebav3916s", this.ebav3916s);
//    this.FunctionDictionary.Add("ebav0880m", this.ebav0880m);
//    this.FunctionDictionary.Add("ebav0881m", this.ebav0881m);
//    this.FunctionDictionary.Add("ebav0882m", this.ebav0882m);
//    this.FunctionDictionary.Add("ebav0884m", this.ebav0884m);
//    this.FunctionDictionary.Add("ebav0885m", this.ebav0885m);
//    this.FunctionDictionary.Add("ebav0886m", this.ebav0886m);
//    this.FunctionDictionary.Add("ebav3953s", this.ebav3953s);
//    this.FunctionDictionary.Add("ebav3954s", this.ebav3954s);
//    this.FunctionDictionary.Add("ebav3955s", this.ebav3955s);
//    this.FunctionDictionary.Add("ebav0887m", this.ebav0887m);
//    this.FunctionDictionary.Add("ebav0888m", this.ebav0888m);
//    this.FunctionDictionary.Add("ebav1713m", this.ebav1713m);
//    this.FunctionDictionary.Add("ebav3956s", this.ebav3956s);
//    this.FunctionDictionary.Add("ebav0890m", this.ebav0890m);
//    this.FunctionDictionary.Add("ebav0891m", this.ebav0891m);
//    this.FunctionDictionary.Add("ebav0892m", this.ebav0892m);
//    this.FunctionDictionary.Add("ebav0893m", this.ebav0893m);
//    this.FunctionDictionary.Add("ebav0894m", this.ebav0894m);
//    this.FunctionDictionary.Add("ebav0895m", this.ebav0895m);
//    this.FunctionDictionary.Add("ebav1959h", this.ebav1959h);
//    this.FunctionDictionary.Add("ebav1960h", this.ebav1960h);
//    this.FunctionDictionary.Add("ebav1961h", this.ebav1961h);
//    this.FunctionDictionary.Add("ebav1962h", this.ebav1962h);
//    this.FunctionDictionary.Add("ebav1963h", this.ebav1963h);
//    this.FunctionDictionary.Add("ebav1964h", this.ebav1964h);
//    this.FunctionDictionary.Add("ebav3936s", this.ebav3936s);
//    this.FunctionDictionary.Add("ebav3937s", this.ebav3937s);
//    this.FunctionDictionary.Add("ebav3938s", this.ebav3938s);
//    this.FunctionDictionary.Add("ebav3939s", this.ebav3939s);
//    this.FunctionDictionary.Add("ebav0896m", this.ebav0896m);
//    this.FunctionDictionary.Add("ebav1188m", this.ebav1188m);
//    this.FunctionDictionary.Add("ebav1189m", this.ebav1189m);
//    this.FunctionDictionary.Add("ebav1190m", this.ebav1190m);
//    this.FunctionDictionary.Add("ebav1191m", this.ebav1191m);
//    this.FunctionDictionary.Add("ebav1192m", this.ebav1192m);
//    this.FunctionDictionary.Add("ebav1193m", this.ebav1193m);
//    this.FunctionDictionary.Add("ebav1194m", this.ebav1194m);
//    this.FunctionDictionary.Add("ebav1195m", this.ebav1195m);
//    this.FunctionDictionary.Add("ebav1196m", this.ebav1196m);
//    this.FunctionDictionary.Add("ebav1197m", this.ebav1197m);
//    this.FunctionDictionary.Add("ebav1200m", this.ebav1200m);
//    this.FunctionDictionary.Add("ebav1201m", this.ebav1201m);
//    this.FunctionDictionary.Add("ebav1202m", this.ebav1202m);
//    this.FunctionDictionary.Add("ebav1203m", this.ebav1203m);
//    this.FunctionDictionary.Add("ebav1204m", this.ebav1204m);
//    this.FunctionDictionary.Add("ebav1206m", this.ebav1206m);
//    this.FunctionDictionary.Add("ebav1965h", this.ebav1965h);
//    this.FunctionDictionary.Add("ebav1966h", this.ebav1966h);
//    this.FunctionDictionary.Add("ebav1967h", this.ebav1967h);
//    this.FunctionDictionary.Add("ebav3944s", this.ebav3944s);
//    this.FunctionDictionary.Add("ebav3945s", this.ebav3945s);
//    this.FunctionDictionary.Add("ebav3946s", this.ebav3946s);
//    this.FunctionDictionary.Add("ebav3947s", this.ebav3947s);
//    this.FunctionDictionary.Add("ebav0897m", this.ebav0897m);
//    this.FunctionDictionary.Add("ebav0898m", this.ebav0898m);
//    this.FunctionDictionary.Add("ebav0899m", this.ebav0899m);
//    this.FunctionDictionary.Add("ebav0900m", this.ebav0900m);
//    this.FunctionDictionary.Add("ebav0901m", this.ebav0901m);
//    this.FunctionDictionary.Add("ebav0902m", this.ebav0902m);
//    this.FunctionDictionary.Add("ebav0903m", this.ebav0903m);
//    this.FunctionDictionary.Add("ebav0904m", this.ebav0904m);
//    this.FunctionDictionary.Add("ebav0905m", this.ebav0905m);
//    this.FunctionDictionary.Add("ebav0906m", this.ebav0906m);
//    this.FunctionDictionary.Add("ebav0907m", this.ebav0907m);
//    this.FunctionDictionary.Add("ebav0908m", this.ebav0908m);
//    this.FunctionDictionary.Add("ebav0909m", this.ebav0909m);
//    this.FunctionDictionary.Add("ebav1714m", this.ebav1714m);
//    this.FunctionDictionary.Add("ebav1715m", this.ebav1715m);
//    this.FunctionDictionary.Add("ebav1716m", this.ebav1716m);
//    this.FunctionDictionary.Add("ebav1717m", this.ebav1717m);
//    this.FunctionDictionary.Add("ebav1718m", this.ebav1718m);
//    this.FunctionDictionary.Add("ebav1719m", this.ebav1719m);
//    this.FunctionDictionary.Add("ebav1720m", this.ebav1720m);
//    this.FunctionDictionary.Add("ebav1721m", this.ebav1721m);
//    this.FunctionDictionary.Add("ebav1722m", this.ebav1722m);
//    this.FunctionDictionary.Add("ebav1723m", this.ebav1723m);
//    this.FunctionDictionary.Add("ebav1724m", this.ebav1724m);
//    this.FunctionDictionary.Add("ebav1725m", this.ebav1725m);
//    this.FunctionDictionary.Add("ebav1726m", this.ebav1726m);
//    this.FunctionDictionary.Add("ebav1727m", this.ebav1727m);
//    this.FunctionDictionary.Add("ebav0911m", this.ebav0911m);
//    this.FunctionDictionary.Add("ebav1207m", this.ebav1207m);
//    this.FunctionDictionary.Add("ebav1208m", this.ebav1208m);
//    this.FunctionDictionary.Add("ebav1209m", this.ebav1209m);
//    this.FunctionDictionary.Add("ebav1211m", this.ebav1211m);
//    this.FunctionDictionary.Add("ebav1212m", this.ebav1212m);
//    this.FunctionDictionary.Add("ebav1213m", this.ebav1213m);
//    this.FunctionDictionary.Add("ebav1214m", this.ebav1214m);
//    this.FunctionDictionary.Add("ebav1215m", this.ebav1215m);
//    this.FunctionDictionary.Add("ebav1216m", this.ebav1216m);
//    this.FunctionDictionary.Add("ebav1218m", this.ebav1218m);
//    this.FunctionDictionary.Add("ebav1968h", this.ebav1968h);
//    this.FunctionDictionary.Add("ebav1969h", this.ebav1969h);
//    this.FunctionDictionary.Add("ebav1970h", this.ebav1970h);
//    this.FunctionDictionary.Add("ebav3948s", this.ebav3948s);
//    this.FunctionDictionary.Add("ebav3949s", this.ebav3949s);
//    this.FunctionDictionary.Add("ebav0912m", this.ebav0912m);
//    this.FunctionDictionary.Add("ebav0914m", this.ebav0914m);
//    this.FunctionDictionary.Add("ebav0915m", this.ebav0915m);
//    this.FunctionDictionary.Add("ebav0916m", this.ebav0916m);
//    this.FunctionDictionary.Add("ebav0917m", this.ebav0917m);
//    this.FunctionDictionary.Add("ebav0918m", this.ebav0918m);
//    this.FunctionDictionary.Add("ebav0919m", this.ebav0919m);
//    this.FunctionDictionary.Add("ebav0920m", this.ebav0920m);
//    this.FunctionDictionary.Add("ebav0921m", this.ebav0921m);
//    this.FunctionDictionary.Add("ebav0922m", this.ebav0922m);
//    this.FunctionDictionary.Add("ebav0923m", this.ebav0923m);
//    this.FunctionDictionary.Add("ebav3943s", this.ebav3943s);
//    this.FunctionDictionary.Add("ebav0924m", this.ebav0924m);
//    this.FunctionDictionary.Add("ebav0925m", this.ebav0925m);
//    this.FunctionDictionary.Add("ebav0926m", this.ebav0926m);
//    this.FunctionDictionary.Add("ebav0927m", this.ebav0927m);
//    this.FunctionDictionary.Add("ebav0928m", this.ebav0928m);
//    this.FunctionDictionary.Add("ebav0929m", this.ebav0929m);
//    this.FunctionDictionary.Add("ebav0930m", this.ebav0930m);
//    this.FunctionDictionary.Add("ebav0931m", this.ebav0931m);
//    this.FunctionDictionary.Add("ebav0932m", this.ebav0932m);
//    this.FunctionDictionary.Add("ebav0933m", this.ebav0933m);
//    this.FunctionDictionary.Add("ebav0934m", this.ebav0934m);
//    this.FunctionDictionary.Add("ebav0935m", this.ebav0935m);
//    this.FunctionDictionary.Add("ebav0936m", this.ebav0936m);
//    this.FunctionDictionary.Add("ebav0937m", this.ebav0937m);
//    this.FunctionDictionary.Add("ebav0938m", this.ebav0938m);
//    this.FunctionDictionary.Add("ebav0939m", this.ebav0939m);
//    this.FunctionDictionary.Add("ebav0940m", this.ebav0940m);
//    this.FunctionDictionary.Add("ebav0941m", this.ebav0941m);
//    this.FunctionDictionary.Add("ebav0942m", this.ebav0942m);
//    this.FunctionDictionary.Add("ebav0943m", this.ebav0943m);
//    this.FunctionDictionary.Add("ebav0944m", this.ebav0944m);
//    this.FunctionDictionary.Add("ebav0945m", this.ebav0945m);
//    this.FunctionDictionary.Add("ebav0946m", this.ebav0946m);
//    this.FunctionDictionary.Add("ebav0947m", this.ebav0947m);
//    this.FunctionDictionary.Add("ebav0948m", this.ebav0948m);
//    this.FunctionDictionary.Add("ebav0949m", this.ebav0949m);
//    this.FunctionDictionary.Add("ebav0950m", this.ebav0950m);
//    this.FunctionDictionary.Add("ebav0951m", this.ebav0951m);
//    this.FunctionDictionary.Add("ebav0952m", this.ebav0952m);
//    this.FunctionDictionary.Add("ebav0953m", this.ebav0953m);
//    this.FunctionDictionary.Add("ebav1728m", this.ebav1728m);
//    this.FunctionDictionary.Add("ebav1729m", this.ebav1729m);
//    this.FunctionDictionary.Add("ebav1730m", this.ebav1730m);
//    this.FunctionDictionary.Add("ebav1731m", this.ebav1731m);
//    this.FunctionDictionary.Add("ebav1978h", this.ebav1978h);
//    this.FunctionDictionary.Add("ebav1979h", this.ebav1979h);
//    this.FunctionDictionary.Add("ebav1980h", this.ebav1980h);
//    this.FunctionDictionary.Add("ebav3960s", this.ebav3960s);
//    this.FunctionDictionary.Add("ebav0954m", this.ebav0954m);
//    this.FunctionDictionary.Add("ebav0955m", this.ebav0955m);
//    this.FunctionDictionary.Add("ebav0956m", this.ebav0956m);
//    this.FunctionDictionary.Add("ebav0957m", this.ebav0957m);
//    this.FunctionDictionary.Add("ebav3961s", this.ebav3961s);
//    this.FunctionDictionary.Add("ebav0958m", this.ebav0958m);
//    this.FunctionDictionary.Add("ebav0959m", this.ebav0959m);
//    this.FunctionDictionary.Add("ebav0960m", this.ebav0960m);
//    this.FunctionDictionary.Add("ebav0961m", this.ebav0961m);
//    this.FunctionDictionary.Add("ebav0962m", this.ebav0962m);
//    this.FunctionDictionary.Add("ebav0963m", this.ebav0963m);
//    this.FunctionDictionary.Add("ebav0964m", this.ebav0964m);
//    this.FunctionDictionary.Add("ebav0965m", this.ebav0965m);
//    this.FunctionDictionary.Add("ebav0966m", this.ebav0966m);
//    this.FunctionDictionary.Add("ebav0967m", this.ebav0967m);
//    this.FunctionDictionary.Add("ebav0968m", this.ebav0968m);
//    this.FunctionDictionary.Add("ebav0969m", this.ebav0969m);
//    this.FunctionDictionary.Add("ebav0970m", this.ebav0970m);
//    this.FunctionDictionary.Add("ebav0971m", this.ebav0971m);
//    this.FunctionDictionary.Add("ebav0972m", this.ebav0972m);
//    this.FunctionDictionary.Add("ebav0973m", this.ebav0973m);
//    this.FunctionDictionary.Add("ebav0974m", this.ebav0974m);
//    this.FunctionDictionary.Add("ebav0975m", this.ebav0975m);
//    this.FunctionDictionary.Add("ebav0976m", this.ebav0976m);
//    this.FunctionDictionary.Add("ebav0977m", this.ebav0977m);
//    this.FunctionDictionary.Add("ebav0978m", this.ebav0978m);
//    this.FunctionDictionary.Add("ebav0979m", this.ebav0979m);
//    this.FunctionDictionary.Add("ebav0980m", this.ebav0980m);
//    this.FunctionDictionary.Add("ebav1732m", this.ebav1732m);
//    this.FunctionDictionary.Add("ebav1981h", this.ebav1981h);
//    this.FunctionDictionary.Add("ebav1982h", this.ebav1982h);
//    this.FunctionDictionary.Add("ebav3962s", this.ebav3962s);
//    this.FunctionDictionary.Add("ebav0985m", this.ebav0985m);
//    this.FunctionDictionary.Add("ebav0986m", this.ebav0986m);
//    this.FunctionDictionary.Add("ebav0987m", this.ebav0987m);
//    this.FunctionDictionary.Add("ebav0988m", this.ebav0988m);
//    this.FunctionDictionary.Add("ebav3136m", this.ebav3136m);
//    this.FunctionDictionary.Add("ebav3137m", this.ebav3137m);
//    this.FunctionDictionary.Add("ebav3149m", this.ebav3149m);
//    this.FunctionDictionary.Add("ebav3150m", this.ebav3150m);
//    this.FunctionDictionary.Add("ebav3151m", this.ebav3151m);
//    this.FunctionDictionary.Add("ebav3152m", this.ebav3152m);
//    this.FunctionDictionary.Add("ebav3964s", this.ebav3964s);
//    this.FunctionDictionary.Add("ebav0993m", this.ebav0993m);
//    this.FunctionDictionary.Add("ebav0994m", this.ebav0994m);
//    this.FunctionDictionary.Add("ebav0995m", this.ebav0995m);
//    this.FunctionDictionary.Add("ebav3967s", this.ebav3967s);
//    this.FunctionDictionary.Add("ebav0996m", this.ebav0996m);
//    this.FunctionDictionary.Add("ebav0997m", this.ebav0997m);
//    this.FunctionDictionary.Add("ebav0998m", this.ebav0998m);
//    this.FunctionDictionary.Add("ebav3972s", this.ebav3972s);
//    this.FunctionDictionary.Add("ebav0999m", this.ebav0999m);
//    this.FunctionDictionary.Add("ebav1001m", this.ebav1001m);
//    this.FunctionDictionary.Add("ebav3141m", this.ebav3141m);
//    this.FunctionDictionary.Add("ebav3154m", this.ebav3154m);
//    this.FunctionDictionary.Add("ebav3155m", this.ebav3155m);
//    this.FunctionDictionary.Add("ebav3973s", this.ebav3973s);
//    this.FunctionDictionary.Add("ebav1003m", this.ebav1003m);
//    this.FunctionDictionary.Add("ebav1004m", this.ebav1004m);
//    this.FunctionDictionary.Add("ebav1005m", this.ebav1005m);
//    this.FunctionDictionary.Add("ebav1006m", this.ebav1006m);
//    this.FunctionDictionary.Add("ebav1007m", this.ebav1007m);
//    this.FunctionDictionary.Add("ebav1008m", this.ebav1008m);
//    this.FunctionDictionary.Add("ebav1009m", this.ebav1009m);
//    this.FunctionDictionary.Add("ebav1010m", this.ebav1010m);
//    this.FunctionDictionary.Add("ebav1014m", this.ebav1014m);
//    this.FunctionDictionary.Add("ebav1015m", this.ebav1015m);
//    this.FunctionDictionary.Add("ebav3145m", this.ebav3145m);
//    this.FunctionDictionary.Add("ebav1017m", this.ebav1017m);
//    this.FunctionDictionary.Add("ebav1018m", this.ebav1018m);
//    this.FunctionDictionary.Add("ebav1019m", this.ebav1019m);
//    this.FunctionDictionary.Add("ebav1020m", this.ebav1020m);
//    this.FunctionDictionary.Add("ebav1021m", this.ebav1021m);
//    this.FunctionDictionary.Add("ebav1022m", this.ebav1022m);
//    this.FunctionDictionary.Add("ebav1024m", this.ebav1024m);
//    this.FunctionDictionary.Add("ebav1025m", this.ebav1025m);
//    this.FunctionDictionary.Add("ebav1992h", this.ebav1992h);
//    this.FunctionDictionary.Add("ebav3974s", this.ebav3974s);
//    this.FunctionDictionary.Add("ebav1026m", this.ebav1026m);
//    this.FunctionDictionary.Add("ebav1028m", this.ebav1028m);
//    this.FunctionDictionary.Add("ebav1029m", this.ebav1029m);
//    this.FunctionDictionary.Add("ebav1027m", this.ebav1027m);
//    this.FunctionDictionary.Add("ebav1030m", this.ebav1030m);
//    this.FunctionDictionary.Add("ebav1032m", this.ebav1032m);
//    this.FunctionDictionary.Add("ebav1033m", this.ebav1033m);
//    this.FunctionDictionary.Add("ebav1034m", this.ebav1034m);
//    this.FunctionDictionary.Add("ebav1036m", this.ebav1036m);
//    this.FunctionDictionary.Add("ebav1038m", this.ebav1038m);
//    this.FunctionDictionary.Add("ebav1039m", this.ebav1039m);
//    this.FunctionDictionary.Add("ebav1040m", this.ebav1040m);
//    this.FunctionDictionary.Add("ebav3950s", this.ebav3950s);
//    this.FunctionDictionary.Add("ebav3951s", this.ebav3951s);
//    this.FunctionDictionary.Add("ebav1041m", this.ebav1041m);
//    this.FunctionDictionary.Add("ebav1042m", this.ebav1042m);
//    this.FunctionDictionary.Add("ebav1043m", this.ebav1043m);
//    this.FunctionDictionary.Add("ebav1044m", this.ebav1044m);
//    this.FunctionDictionary.Add("ebav1045m", this.ebav1045m);
//    this.FunctionDictionary.Add("ebav1046m", this.ebav1046m);
//    this.FunctionDictionary.Add("ebav1971h", this.ebav1971h);
//    this.FunctionDictionary.Add("ebav1047m", this.ebav1047m);
//    this.FunctionDictionary.Add("ebav1048m", this.ebav1048m);
//    this.FunctionDictionary.Add("ebav1985h", this.ebav1985h);
//    this.FunctionDictionary.Add("ebav1986h", this.ebav1986h);
//    this.FunctionDictionary.Add("ebav1987h", this.ebav1987h);
//    this.FunctionDictionary.Add("ebav3970s", this.ebav3970s);
//    this.FunctionDictionary.Add("ebav1049m", this.ebav1049m);
//    this.FunctionDictionary.Add("ebav1050m", this.ebav1050m);
//    this.FunctionDictionary.Add("ebav1051m", this.ebav1051m);
//    this.FunctionDictionary.Add("ebav1052m", this.ebav1052m);
//    this.FunctionDictionary.Add("ebav1053m", this.ebav1053m);
//    this.FunctionDictionary.Add("ebav1054m", this.ebav1054m);
//    this.FunctionDictionary.Add("ebav1055m", this.ebav1055m);
//    this.FunctionDictionary.Add("ebav1056m", this.ebav1056m);
//    this.FunctionDictionary.Add("ebav1058m", this.ebav1058m);
//    this.FunctionDictionary.Add("ebav1059m", this.ebav1059m);
//    this.FunctionDictionary.Add("ebav1060m", this.ebav1060m);
//    this.FunctionDictionary.Add("ebav1061m", this.ebav1061m);
//    this.FunctionDictionary.Add("ebav1062m", this.ebav1062m);
//    this.FunctionDictionary.Add("ebav1063m", this.ebav1063m);
//    this.FunctionDictionary.Add("ebav1064m", this.ebav1064m);
//    this.FunctionDictionary.Add("ebav1065m", this.ebav1065m);
//    this.FunctionDictionary.Add("ebav1066m", this.ebav1066m);
//    this.FunctionDictionary.Add("ebav1067m", this.ebav1067m);
//    this.FunctionDictionary.Add("ebav1225m", this.ebav1225m);
//    this.FunctionDictionary.Add("ebav1749m", this.ebav1749m);
//    this.FunctionDictionary.Add("ebav1750m", this.ebav1750m);
//    this.FunctionDictionary.Add("ebav1751m", this.ebav1751m);
//    this.FunctionDictionary.Add("ebav1752m", this.ebav1752m);
//    this.FunctionDictionary.Add("ebav1754m", this.ebav1754m);
//    this.FunctionDictionary.Add("ebav1756m", this.ebav1756m);
//    this.FunctionDictionary.Add("ebav1757m", this.ebav1757m);
//    this.FunctionDictionary.Add("ebav1758m", this.ebav1758m);
//    this.FunctionDictionary.Add("ebav1759m", this.ebav1759m);
//    this.FunctionDictionary.Add("ebav1760m", this.ebav1760m);
//    this.FunctionDictionary.Add("ebav1761m", this.ebav1761m);
//    this.FunctionDictionary.Add("ebav1762m", this.ebav1762m);
//    this.FunctionDictionary.Add("ebav1763m", this.ebav1763m);
//    this.FunctionDictionary.Add("ebav1998h", this.ebav1998h);
//    this.FunctionDictionary.Add("ebav2027s", this.ebav2027s);
//    this.FunctionDictionary.Add("ebav2028s", this.ebav2028s);
//    this.FunctionDictionary.Add("ebav2062s", this.ebav2062s);
//    this.FunctionDictionary.Add("ebav3990s", this.ebav3990s);
//    this.FunctionDictionary.Add("ebav1068m", this.ebav1068m);
//    this.FunctionDictionary.Add("ebav3940s", this.ebav3940s);
//    this.FunctionDictionary.Add("ebav1069m", this.ebav1069m);
//    this.FunctionDictionary.Add("ebav1070m", this.ebav1070m);
//    this.FunctionDictionary.Add("ebav1071m", this.ebav1071m);
//    this.FunctionDictionary.Add("ebav1072m", this.ebav1072m);
//    this.FunctionDictionary.Add("ebav1073m", this.ebav1073m);
//    this.FunctionDictionary.Add("ebav1074m", this.ebav1074m);
//    this.FunctionDictionary.Add("ebav1075m", this.ebav1075m);
//    this.FunctionDictionary.Add("ebav1078m", this.ebav1078m);
//    this.FunctionDictionary.Add("ebav1081m", this.ebav1081m);
//    this.FunctionDictionary.Add("ebav1084m", this.ebav1084m);
//    this.FunctionDictionary.Add("ebav1086m", this.ebav1086m);
//    this.FunctionDictionary.Add("ebav1087m", this.ebav1087m);
//    this.FunctionDictionary.Add("ebav3941s", this.ebav3941s);
//    this.FunctionDictionary.Add("ebav1088m", this.ebav1088m);
//    this.FunctionDictionary.Add("ebav1093m", this.ebav1093m);
//    this.FunctionDictionary.Add("ebav1094m", this.ebav1094m);
//    this.FunctionDictionary.Add("ebav1095m", this.ebav1095m);
//    this.FunctionDictionary.Add("ebav1996h", this.ebav1996h);
//    this.FunctionDictionary.Add("ebav1997h", this.ebav1997h);
//    this.FunctionDictionary.Add("ebav3978s", this.ebav3978s);
//    this.FunctionDictionary.Add("ebav1096m", this.ebav1096m);
//    this.FunctionDictionary.Add("ebav1097m", this.ebav1097m);
//    this.FunctionDictionary.Add("ebav1098m", this.ebav1098m);
//    this.FunctionDictionary.Add("ebav3979s", this.ebav3979s);
//    this.FunctionDictionary.Add("ebav3980s", this.ebav3980s);
//    this.FunctionDictionary.Add("ebav1099m", this.ebav1099m);
//    this.FunctionDictionary.Add("ebav3982s", this.ebav3982s);
//    this.FunctionDictionary.Add("ebav1103m", this.ebav1103m);
//    this.FunctionDictionary.Add("ebav1983h", this.ebav1983h);
//    this.FunctionDictionary.Add("ebav1984h", this.ebav1984h);
//    this.FunctionDictionary.Add("ebav3969s", this.ebav3969s);
//    this.FunctionDictionary.Add("ebav1104m", this.ebav1104m);
//    this.FunctionDictionary.Add("ebav1105m", this.ebav1105m);
//    this.FunctionDictionary.Add("ebav1106m", this.ebav1106m);
//    this.FunctionDictionary.Add("ebav1107m", this.ebav1107m);
//    this.FunctionDictionary.Add("ebav1108m", this.ebav1108m);
//    this.FunctionDictionary.Add("ebav1109m", this.ebav1109m);
//    this.FunctionDictionary.Add("ebav1110m", this.ebav1110m);
//    this.FunctionDictionary.Add("ebav1111m", this.ebav1111m);
//    this.FunctionDictionary.Add("ebav1112m", this.ebav1112m);
//    this.FunctionDictionary.Add("ebav1113m", this.ebav1113m);
//    this.FunctionDictionary.Add("ebav1114m", this.ebav1114m);
//    this.FunctionDictionary.Add("ebav1115m", this.ebav1115m);
//    this.FunctionDictionary.Add("ebav1116m", this.ebav1116m);
//    this.FunctionDictionary.Add("ebav1764m", this.ebav1764m);
//    this.FunctionDictionary.Add("ebav3983s", this.ebav3983s);
//    this.FunctionDictionary.Add("ebav3984s", this.ebav3984s);
//    this.FunctionDictionary.Add("ebav1117m", this.ebav1117m);
//    this.FunctionDictionary.Add("ebav1118m", this.ebav1118m);
//    this.FunctionDictionary.Add("ebav3985s", this.ebav3985s);
//    this.FunctionDictionary.Add("ebav1121m", this.ebav1121m);
//    this.FunctionDictionary.Add("ebav3986s", this.ebav3986s);
//    this.FunctionDictionary.Add("ebav3987s", this.ebav3987s);
//    this.FunctionDictionary.Add("ebav1122m", this.ebav1122m);
//    this.FunctionDictionary.Add("ebav1123m", this.ebav1123m);
//    this.FunctionDictionary.Add("ebav3316h", this.ebav3316h);
//    this.FunctionDictionary.Add("ebav1124m", this.ebav1124m);
//    this.FunctionDictionary.Add("ebav1125m", this.ebav1125m);
//    this.FunctionDictionary.Add("ebav1126m", this.ebav1126m);
//    this.FunctionDictionary.Add("ebav1127m", this.ebav1127m);
//    this.FunctionDictionary.Add("ebav1128m", this.ebav1128m);
//    this.FunctionDictionary.Add("ebav1765m", this.ebav1765m);
//    this.FunctionDictionary.Add("ebav1766m", this.ebav1766m);
//    this.FunctionDictionary.Add("ebav1767m", this.ebav1767m);
//    this.FunctionDictionary.Add("ebav1768m", this.ebav1768m);
//    this.FunctionDictionary.Add("ebav1972h", this.ebav1972h);
//    this.FunctionDictionary.Add("ebav1973h", this.ebav1973h);
//    this.FunctionDictionary.Add("ebav1974h", this.ebav1974h);
//    this.FunctionDictionary.Add("ebav1975h", this.ebav1975h);
//    this.FunctionDictionary.Add("ebav1976h", this.ebav1976h);
//    this.FunctionDictionary.Add("ebav3957s", this.ebav3957s);
//    this.FunctionDictionary.Add("ebav1129m", this.ebav1129m);
//    this.FunctionDictionary.Add("ebav3958s", this.ebav3958s);
//    this.FunctionDictionary.Add("ebav1130m", this.ebav1130m);
//    this.FunctionDictionary.Add("ebav1131m", this.ebav1131m);
//    this.FunctionDictionary.Add("ebav1132m", this.ebav1132m);
//    this.FunctionDictionary.Add("ebav1133m", this.ebav1133m);
//    this.FunctionDictionary.Add("ebav1134m", this.ebav1134m);
//    this.FunctionDictionary.Add("ebav1135m", this.ebav1135m);
//    this.FunctionDictionary.Add("ebav1769m", this.ebav1769m);
//    this.FunctionDictionary.Add("ebav1770m", this.ebav1770m);
//    this.FunctionDictionary.Add("ebav1977h", this.ebav1977h);
//    this.FunctionDictionary.Add("ebav2061s", this.ebav2061s);
//    this.FunctionDictionary.Add("ebav3959s", this.ebav3959s);
//    this.FunctionDictionary.Add("ebav1137m", this.ebav1137m);
//    this.FunctionDictionary.Add("ebav1139m", this.ebav1139m);
//    this.FunctionDictionary.Add("ebav1140m", this.ebav1140m);
//    this.FunctionDictionary.Add("ebav3989s", this.ebav3989s);
//    this.FunctionDictionary.Add("ebav1160m", this.ebav1160m);
//    this.FunctionDictionary.Add("ebav3143m", this.ebav3143m);
//    this.FunctionDictionary.Add("ebav3144m", this.ebav3144m);
//    this.FunctionDictionary.Add("ebav1241m", this.ebav1241m);
//    this.FunctionDictionary.Add("ebav1242m", this.ebav1242m);
//    this.FunctionDictionary.Add("ebav1995h", this.ebav1995h);
//    this.FunctionDictionary.Add("ebav3976s", this.ebav3976s);
//    this.FunctionDictionary.Add("ebav4027a", this.ebav4027a);
//    this.FunctionDictionary.Add("ebav1243m", this.ebav1243m);
//    this.FunctionDictionary.Add("ebav3977s", this.ebav3977s);
//    this.FunctionDictionary.Add("ebav1251m", this.ebav1251m);
//    this.FunctionDictionary.Add("ebav1252m", this.ebav1252m);
//    this.FunctionDictionary.Add("ebav1253m", this.ebav1253m);
//    this.FunctionDictionary.Add("ebav1254m", this.ebav1254m);
//    this.FunctionDictionary.Add("ebav1255m", this.ebav1255m);
//    this.FunctionDictionary.Add("ebav1256m", this.ebav1256m);
//    this.FunctionDictionary.Add("ebav1257m", this.ebav1257m);
//    this.FunctionDictionary.Add("ebav1258m", this.ebav1258m);
//    this.FunctionDictionary.Add("ebav1259m", this.ebav1259m);
//    this.FunctionDictionary.Add("ebav1260m", this.ebav1260m);
//    this.FunctionDictionary.Add("ebav1261m", this.ebav1261m);
//    this.FunctionDictionary.Add("ebav1262m", this.ebav1262m);
//    this.FunctionDictionary.Add("ebav1263m", this.ebav1263m);
//    this.FunctionDictionary.Add("ebav1264m", this.ebav1264m);
//    this.FunctionDictionary.Add("ebav1265m", this.ebav1265m);
//    this.FunctionDictionary.Add("ebav1266m", this.ebav1266m);
//    this.FunctionDictionary.Add("ebav1267m", this.ebav1267m);
//    this.FunctionDictionary.Add("ebav1268m", this.ebav1268m);
//    this.FunctionDictionary.Add("ebav1269m", this.ebav1269m);
//    this.FunctionDictionary.Add("ebav1270m", this.ebav1270m);
//    this.FunctionDictionary.Add("ebav1271m", this.ebav1271m);
//    this.FunctionDictionary.Add("ebav1272m", this.ebav1272m);
//    this.FunctionDictionary.Add("ebav1273m", this.ebav1273m);
//    this.FunctionDictionary.Add("ebav1274m", this.ebav1274m);
//    this.FunctionDictionary.Add("ebav1275m", this.ebav1275m);
//    this.FunctionDictionary.Add("ebav1276m", this.ebav1276m);
//    this.FunctionDictionary.Add("ebav1277m", this.ebav1277m);
//    this.FunctionDictionary.Add("ebav1278m", this.ebav1278m);
//    this.FunctionDictionary.Add("ebav1279m", this.ebav1279m);
//    this.FunctionDictionary.Add("ebav1280m", this.ebav1280m);
//    this.FunctionDictionary.Add("ebav1281m", this.ebav1281m);
//    this.FunctionDictionary.Add("ebav1282m", this.ebav1282m);
//    this.FunctionDictionary.Add("ebav1283m", this.ebav1283m);
//    this.FunctionDictionary.Add("ebav1284m", this.ebav1284m);
//    this.FunctionDictionary.Add("ebav1285m", this.ebav1285m);
//    this.FunctionDictionary.Add("ebav1286m", this.ebav1286m);
//    this.FunctionDictionary.Add("ebav1287m", this.ebav1287m);
//    this.FunctionDictionary.Add("ebav1288m", this.ebav1288m);
//    this.FunctionDictionary.Add("ebav1289m", this.ebav1289m);
//    this.FunctionDictionary.Add("ebav1290m", this.ebav1290m);
//    this.FunctionDictionary.Add("ebav1291m", this.ebav1291m);
//    this.FunctionDictionary.Add("ebav1292m", this.ebav1292m);
//    this.FunctionDictionary.Add("ebav1293m", this.ebav1293m);
//    this.FunctionDictionary.Add("ebav1294m", this.ebav1294m);
//    this.FunctionDictionary.Add("ebav1295m", this.ebav1295m);
//    this.FunctionDictionary.Add("ebav1296m", this.ebav1296m);
//    this.FunctionDictionary.Add("ebav1297m", this.ebav1297m);
//    this.FunctionDictionary.Add("ebav1298m", this.ebav1298m);
//    this.FunctionDictionary.Add("ebav1299m", this.ebav1299m);
//    this.FunctionDictionary.Add("ebav1300m", this.ebav1300m);
//    this.FunctionDictionary.Add("ebav1301m", this.ebav1301m);
//    this.FunctionDictionary.Add("ebav1302m", this.ebav1302m);
//    this.FunctionDictionary.Add("ebav1303m", this.ebav1303m);
//    this.FunctionDictionary.Add("ebav1304m", this.ebav1304m);
//    this.FunctionDictionary.Add("ebav1305m", this.ebav1305m);
//    this.FunctionDictionary.Add("ebav1306m", this.ebav1306m);
//    this.FunctionDictionary.Add("ebav1307m", this.ebav1307m);
//    this.FunctionDictionary.Add("ebav1308m", this.ebav1308m);
//    this.FunctionDictionary.Add("ebav1309m", this.ebav1309m);
//    this.FunctionDictionary.Add("ebav1310m", this.ebav1310m);
//    this.FunctionDictionary.Add("ebav1311m", this.ebav1311m);
//    this.FunctionDictionary.Add("ebav1312m", this.ebav1312m);
//    this.FunctionDictionary.Add("ebav1329m", this.ebav1329m);
//    this.FunctionDictionary.Add("ebav1330m", this.ebav1330m);
//    this.FunctionDictionary.Add("ebav1331m", this.ebav1331m);
//    this.FunctionDictionary.Add("ebav1332m", this.ebav1332m);
//    this.FunctionDictionary.Add("ebav1333m", this.ebav1333m);
//    this.FunctionDictionary.Add("ebav1334m", this.ebav1334m);
//    this.FunctionDictionary.Add("ebav1335m", this.ebav1335m);
//    this.FunctionDictionary.Add("ebav1925h", this.ebav1925h);
//    this.FunctionDictionary.Add("ebav1926h", this.ebav1926h);
//    this.FunctionDictionary.Add("ebav1927h", this.ebav1927h);
//    this.FunctionDictionary.Add("ebav1928h", this.ebav1928h);
//    this.FunctionDictionary.Add("ebav1337m", this.ebav1337m);
//    this.FunctionDictionary.Add("ebav1338m", this.ebav1338m);
//    this.FunctionDictionary.Add("ebav1339m", this.ebav1339m);
//    this.FunctionDictionary.Add("ebav1340m", this.ebav1340m);
//    this.FunctionDictionary.Add("ebav1341m", this.ebav1341m);
//    this.FunctionDictionary.Add("ebav1342m", this.ebav1342m);
//    this.FunctionDictionary.Add("ebav1343m", this.ebav1343m);
//    this.FunctionDictionary.Add("ebav1344m", this.ebav1344m);
//    this.FunctionDictionary.Add("ebav1345m", this.ebav1345m);
//    this.FunctionDictionary.Add("ebav1346m", this.ebav1346m);
//    this.FunctionDictionary.Add("ebav1347m", this.ebav1347m);
//    this.FunctionDictionary.Add("ebav1348m", this.ebav1348m);
//    this.FunctionDictionary.Add("ebav1349m", this.ebav1349m);
//    this.FunctionDictionary.Add("ebav1350m", this.ebav1350m);
//    this.FunctionDictionary.Add("ebav1351m", this.ebav1351m);
//    this.FunctionDictionary.Add("ebav1352m", this.ebav1352m);
//    this.FunctionDictionary.Add("ebav1353m", this.ebav1353m);
//    this.FunctionDictionary.Add("ebav1354m", this.ebav1354m);
//    this.FunctionDictionary.Add("ebav1355m", this.ebav1355m);
//    this.FunctionDictionary.Add("ebav1356m", this.ebav1356m);
//    this.FunctionDictionary.Add("ebav1357m", this.ebav1357m);
//    this.FunctionDictionary.Add("ebav1358m", this.ebav1358m);
//    this.FunctionDictionary.Add("ebav1359m", this.ebav1359m);
//    this.FunctionDictionary.Add("ebav1360m", this.ebav1360m);
//    this.FunctionDictionary.Add("ebav1361m", this.ebav1361m);
//    this.FunctionDictionary.Add("ebav1362m", this.ebav1362m);
//    this.FunctionDictionary.Add("ebav1363m", this.ebav1363m);
//    this.FunctionDictionary.Add("ebav1364m", this.ebav1364m);
//    this.FunctionDictionary.Add("ebav1365m", this.ebav1365m);
//    this.FunctionDictionary.Add("ebav1366m", this.ebav1366m);
//    this.FunctionDictionary.Add("ebav1367m", this.ebav1367m);
//    this.FunctionDictionary.Add("ebav1368m", this.ebav1368m);
//    this.FunctionDictionary.Add("ebav1369m", this.ebav1369m);
//    this.FunctionDictionary.Add("ebav1370m", this.ebav1370m);
//    this.FunctionDictionary.Add("ebav1371m", this.ebav1371m);
//    this.FunctionDictionary.Add("ebav1372m", this.ebav1372m);
//    this.FunctionDictionary.Add("ebav1373m", this.ebav1373m);
//    this.FunctionDictionary.Add("ebav1374m", this.ebav1374m);
//    this.FunctionDictionary.Add("ebav1375m", this.ebav1375m);
//    this.FunctionDictionary.Add("ebav1376m", this.ebav1376m);
//    this.FunctionDictionary.Add("ebav1383m", this.ebav1383m);
//    this.FunctionDictionary.Add("ebav1384m", this.ebav1384m);
//    this.FunctionDictionary.Add("ebav1385m", this.ebav1385m);
//    this.FunctionDictionary.Add("ebav1386m", this.ebav1386m);
//    this.FunctionDictionary.Add("ebav1734m", this.ebav1734m);
//    this.FunctionDictionary.Add("ebav1736m", this.ebav1736m);
//    this.FunctionDictionary.Add("ebav1738m", this.ebav1738m);
//    this.FunctionDictionary.Add("ebav3146m", this.ebav3146m);
//    this.FunctionDictionary.Add("ebav3147m", this.ebav3147m);
//    this.FunctionDictionary.Add("ebav3148m", this.ebav3148m);
//    this.FunctionDictionary.Add("ebav1742m", this.ebav1742m);
//    this.FunctionDictionary.Add("ebav1743m", this.ebav1743m);
//    this.FunctionDictionary.Add("ebav1744m", this.ebav1744m);
//    this.FunctionDictionary.Add("ebav1745m", this.ebav1745m);
//    this.FunctionDictionary.Add("ebav1746m", this.ebav1746m);
//    this.FunctionDictionary.Add("ebav1747m", this.ebav1747m);
//    this.FunctionDictionary.Add("ebav1748m", this.ebav1748m);
//    this.FunctionDictionary.Add("ebav1911h", this.ebav1911h);
//    this.FunctionDictionary.Add("ebav1912h", this.ebav1912h);
//    this.FunctionDictionary.Add("ebav1913h", this.ebav1913h);
//    this.FunctionDictionary.Add("ebav1914h", this.ebav1914h);
//    this.FunctionDictionary.Add("ebav1915h", this.ebav1915h);
//    this.FunctionDictionary.Add("ebav1916h", this.ebav1916h);
//    this.FunctionDictionary.Add("ebav1917h", this.ebav1917h);
//    this.FunctionDictionary.Add("ebav1918h", this.ebav1918h);
//    this.FunctionDictionary.Add("ebav1919h", this.ebav1919h);
//    this.FunctionDictionary.Add("ebav1922h", this.ebav1922h);
//    this.FunctionDictionary.Add("ebav1923h", this.ebav1923h);
//    this.FunctionDictionary.Add("ebav1924h", this.ebav1924h);
//    this.FunctionDictionary.Add("ebav1933h", this.ebav1933h);
//    this.FunctionDictionary.Add("ebav3907s", this.ebav3907s);
//    this.FunctionDictionary.Add("ebav1935h", this.ebav1935h);
//    this.FunctionDictionary.Add("ebav1936h", this.ebav1936h);
//    this.FunctionDictionary.Add("ebav1937h", this.ebav1937h);
//    this.FunctionDictionary.Add("ebav1938h", this.ebav1938h);
//    this.FunctionDictionary.Add("ebav1939h", this.ebav1939h);
//    this.FunctionDictionary.Add("ebav1940h", this.ebav1940h);
//    this.FunctionDictionary.Add("ebav1941h", this.ebav1941h);
//    this.FunctionDictionary.Add("ebav1949h", this.ebav1949h);
//    this.FunctionDictionary.Add("ebav3926s", this.ebav3926s);
//    this.FunctionDictionary.Add("ebav1988h", this.ebav1988h);
//    this.FunctionDictionary.Add("ebav1989h", this.ebav1989h);
//    this.FunctionDictionary.Add("ebav1990h", this.ebav1990h);
//    this.FunctionDictionary.Add("ebav1991h", this.ebav1991h);
//    this.FunctionDictionary.Add("ebav3971s", this.ebav3971s);
//    this.FunctionDictionary.Add("ebav2701m", this.ebav2701m);
//    this.FunctionDictionary.Add("ebav2702m", this.ebav2702m);
//    this.FunctionDictionary.Add("ebav2703m", this.ebav2703m);
//    this.FunctionDictionary.Add("ebav2706m", this.ebav2706m);
//    this.FunctionDictionary.Add("ebav2707m", this.ebav2707m);
//    this.FunctionDictionary.Add("ebav2709m", this.ebav2709m);
//    this.FunctionDictionary.Add("ebav2712m", this.ebav2712m);
//    this.FunctionDictionary.Add("ebav2715m", this.ebav2715m);
//    this.FunctionDictionary.Add("ebav2718m", this.ebav2718m);
//    this.FunctionDictionary.Add("ebav2721m", this.ebav2721m);
//    this.FunctionDictionary.Add("ebav2724m", this.ebav2724m);
//    this.FunctionDictionary.Add("ebav2727m", this.ebav2727m);
//    this.FunctionDictionary.Add("ebav2730m", this.ebav2730m);
//    this.FunctionDictionary.Add("ebav2733m", this.ebav2733m);
//    this.FunctionDictionary.Add("ebav2736m", this.ebav2736m);
//    this.FunctionDictionary.Add("ebav2739m", this.ebav2739m);
//    this.FunctionDictionary.Add("ebav2742m", this.ebav2742m);
//    this.FunctionDictionary.Add("ebav2744m", this.ebav2744m);
//    this.FunctionDictionary.Add("ebav2746m", this.ebav2746m);
//    this.FunctionDictionary.Add("ebav2748m", this.ebav2748m);
//    this.FunctionDictionary.Add("ebav2704m", this.ebav2704m);
//    this.FunctionDictionary.Add("ebav2705m", this.ebav2705m);
//    this.FunctionDictionary.Add("ebav2710m", this.ebav2710m);
//    this.FunctionDictionary.Add("ebav2713m", this.ebav2713m);
//    this.FunctionDictionary.Add("ebav2716m", this.ebav2716m);
//    this.FunctionDictionary.Add("ebav2719m", this.ebav2719m);
//    this.FunctionDictionary.Add("ebav2722m", this.ebav2722m);
//    this.FunctionDictionary.Add("ebav2725m", this.ebav2725m);
//    this.FunctionDictionary.Add("ebav2728m", this.ebav2728m);
//    this.FunctionDictionary.Add("ebav2731m", this.ebav2731m);
//    this.FunctionDictionary.Add("ebav2734m", this.ebav2734m);
//    this.FunctionDictionary.Add("ebav2737m", this.ebav2737m);
//    this.FunctionDictionary.Add("ebav2740m", this.ebav2740m);
//    this.FunctionDictionary.Add("ebav2750m", this.ebav2750m);
//    this.FunctionDictionary.Add("ebav2751m", this.ebav2751m);
//    this.FunctionDictionary.Add("ebav2752m", this.ebav2752m);
//    this.FunctionDictionary.Add("ebav2753m", this.ebav2753m);
//    this.FunctionDictionary.Add("ebav4154m", this.ebav4154m);
//    this.FunctionDictionary.Add("ebav4155m", this.ebav4155m);
//    this.FunctionDictionary.Add("ebav2711m", this.ebav2711m);
//    this.FunctionDictionary.Add("ebav2714m", this.ebav2714m);
//    this.FunctionDictionary.Add("ebav2717m", this.ebav2717m);
//    this.FunctionDictionary.Add("ebav2720m", this.ebav2720m);
//    this.FunctionDictionary.Add("ebav2723m", this.ebav2723m);
//    this.FunctionDictionary.Add("ebav2726m", this.ebav2726m);
//    this.FunctionDictionary.Add("ebav2729m", this.ebav2729m);
//    this.FunctionDictionary.Add("ebav2732m", this.ebav2732m);
//    this.FunctionDictionary.Add("ebav2735m", this.ebav2735m);
//    this.FunctionDictionary.Add("ebav2738m", this.ebav2738m);
//    this.FunctionDictionary.Add("ebav2741m", this.ebav2741m);
//    this.FunctionDictionary.Add("ebav2743m", this.ebav2743m);
//    this.FunctionDictionary.Add("ebav2745m", this.ebav2745m);
//    this.FunctionDictionary.Add("ebav2747m", this.ebav2747m);
//    this.FunctionDictionary.Add("ebav2749m", this.ebav2749m);
//    this.FunctionDictionary.Add("ebav2781m", this.ebav2781m);
//    this.FunctionDictionary.Add("ebav2782m", this.ebav2782m);
//    this.FunctionDictionary.Add("ebav2783m", this.ebav2783m);
//    this.FunctionDictionary.Add("ebav2784m", this.ebav2784m);
//    this.FunctionDictionary.Add("ebav2785m", this.ebav2785m);
//    this.FunctionDictionary.Add("ebav2786m", this.ebav2786m);
//    this.FunctionDictionary.Add("ebav2787m", this.ebav2787m);
//    this.FunctionDictionary.Add("ebav2788m", this.ebav2788m);
//    this.FunctionDictionary.Add("ebav2789m", this.ebav2789m);
//    this.FunctionDictionary.Add("ebav2790m", this.ebav2790m);
//    this.FunctionDictionary.Add("ebav2791m", this.ebav2791m);
//    this.FunctionDictionary.Add("ebav2792m", this.ebav2792m);
//    this.FunctionDictionary.Add("ebav2793m", this.ebav2793m);
//    this.FunctionDictionary.Add("ebav2794m", this.ebav2794m);
//    this.FunctionDictionary.Add("ebav2795m", this.ebav2795m);
//    this.FunctionDictionary.Add("ebav2796m", this.ebav2796m);
//    this.FunctionDictionary.Add("ebav2797m", this.ebav2797m);
//    this.FunctionDictionary.Add("ebav2798m", this.ebav2798m);
//    this.FunctionDictionary.Add("ebav2799m", this.ebav2799m);
//    this.FunctionDictionary.Add("ebav2800m", this.ebav2800m);
//    this.FunctionDictionary.Add("ebav3018m", this.ebav3018m);
//    this.FunctionDictionary.Add("ebav3019m", this.ebav3019m);
//    this.FunctionDictionary.Add("ebav3020m", this.ebav3020m);
//    this.FunctionDictionary.Add("ebav3021m", this.ebav3021m);
//    this.FunctionDictionary.Add("ebav3078m", this.ebav3078m);
//    this.FunctionDictionary.Add("ebav3079m", this.ebav3079m);
//    this.FunctionDictionary.Add("ebav3080m", this.ebav3080m);
//    this.FunctionDictionary.Add("ebav3081m", this.ebav3081m);
//    this.FunctionDictionary.Add("ebav3082m", this.ebav3082m);
//    this.FunctionDictionary.Add("ebav3083m", this.ebav3083m);
//    this.FunctionDictionary.Add("ebav3084m", this.ebav3084m);
//    this.FunctionDictionary.Add("ebav3085m", this.ebav3085m);
//    this.FunctionDictionary.Add("ebav3086m", this.ebav3086m);
//    this.FunctionDictionary.Add("ebav3089m", this.ebav3089m);
//    this.FunctionDictionary.Add("ebav3090m", this.ebav3090m);
//    this.FunctionDictionary.Add("ebav3092m", this.ebav3092m);
//    this.FunctionDictionary.Add("ebav3095m", this.ebav3095m);
//    this.FunctionDictionary.Add("ebav3098m", this.ebav3098m);
//    this.FunctionDictionary.Add("ebav3101m", this.ebav3101m);
//    this.FunctionDictionary.Add("ebav3104m", this.ebav3104m);
//    this.FunctionDictionary.Add("ebav3107m", this.ebav3107m);
//    this.FunctionDictionary.Add("ebav3110m", this.ebav3110m);
//    this.FunctionDictionary.Add("ebav3113m", this.ebav3113m);
//    this.FunctionDictionary.Add("ebav3115m", this.ebav3115m);
//    this.FunctionDictionary.Add("ebav3117m", this.ebav3117m);
//    this.FunctionDictionary.Add("ebav3119m", this.ebav3119m);
//    this.FunctionDictionary.Add("ebav3087m", this.ebav3087m);
//    this.FunctionDictionary.Add("ebav3088m", this.ebav3088m);
//    this.FunctionDictionary.Add("ebav3093m", this.ebav3093m);
//    this.FunctionDictionary.Add("ebav3096m", this.ebav3096m);
//    this.FunctionDictionary.Add("ebav3099m", this.ebav3099m);
//    this.FunctionDictionary.Add("ebav3102m", this.ebav3102m);
//    this.FunctionDictionary.Add("ebav3105m", this.ebav3105m);
//    this.FunctionDictionary.Add("ebav3108m", this.ebav3108m);
//    this.FunctionDictionary.Add("ebav3111m", this.ebav3111m);
//    this.FunctionDictionary.Add("ebav3121m", this.ebav3121m);
//    this.FunctionDictionary.Add("ebav3122m", this.ebav3122m);
//    this.FunctionDictionary.Add("ebav3123m", this.ebav3123m);
//    this.FunctionDictionary.Add("ebav3124m", this.ebav3124m);
//    this.FunctionDictionary.Add("ebav4156m", this.ebav4156m);
//    this.FunctionDictionary.Add("ebav4157m", this.ebav4157m);
//    this.FunctionDictionary.Add("ebav3094m", this.ebav3094m);
//    this.FunctionDictionary.Add("ebav3097m", this.ebav3097m);
//    this.FunctionDictionary.Add("ebav3100m", this.ebav3100m);
//    this.FunctionDictionary.Add("ebav3103m", this.ebav3103m);
//    this.FunctionDictionary.Add("ebav3106m", this.ebav3106m);
//    this.FunctionDictionary.Add("ebav3109m", this.ebav3109m);
//    this.FunctionDictionary.Add("ebav3112m", this.ebav3112m);
//    this.FunctionDictionary.Add("ebav3114m", this.ebav3114m);
//    this.FunctionDictionary.Add("ebav3116m", this.ebav3116m);
//    this.FunctionDictionary.Add("ebav3118m", this.ebav3118m);
//    this.FunctionDictionary.Add("ebav3120m", this.ebav3120m);
//    this.FunctionDictionary.Add("ebav3125m", this.ebav3125m);
//    this.FunctionDictionary.Add("ebav3126m", this.ebav3126m);
//    this.FunctionDictionary.Add("ebav3127m", this.ebav3127m);
//    this.FunctionDictionary.Add("ebav3128m", this.ebav3128m);
//    this.FunctionDictionary.Add("ebav3129m", this.ebav3129m);
//    this.FunctionDictionary.Add("ebav3130m", this.ebav3130m);
//    this.FunctionDictionary.Add("ebav3131m", this.ebav3131m);
//    this.FunctionDictionary.Add("ebav3138m", this.ebav3138m);
//    this.FunctionDictionary.Add("ebav3139m", this.ebav3139m);
//    this.FunctionDictionary.Add("ebav3965s", this.ebav3965s);
//    this.FunctionDictionary.Add("ebav3153m", this.ebav3153m);
//    this.FunctionDictionary.Add("ebav3968s", this.ebav3968s);
//    this.FunctionDictionary.Add("ebav3925s", this.ebav3925s);
//    this.FunctionDictionary.Add("ebav3942s", this.ebav3942s);
//    this.FunctionDictionary.Add("ebav3952s", this.ebav3952s);
//    this.FunctionDictionary.Add("ebav3966s", this.ebav3966s);
//    this.FunctionDictionary.Add("ebav3975s", this.ebav3975s);
//    this.FunctionDictionary.Add("ebav3981s", this.ebav3981s);
//    this.FunctionDictionary.Add("ebav3988s", this.ebav3988s);
//    this.FunctionDictionary.Add("ebav4004c", this.ebav4004c);
//    this.FunctionDictionary.Add("ebav4005c", this.ebav4005c);
//    this.FunctionDictionary.Add("ebav4006c", this.ebav4006c);

//      }
//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0050h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0067h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0069h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0071h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0763m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0764m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0765m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0766m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0767m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, $r, $s, $t)))
//      public bool ebav0768m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
//         var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, p_r, p_s, p_t));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1694m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1695m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1696m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1697m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav4030m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0057h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav0058h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav0059h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0129h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0130h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0145h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0779m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o)))
//      public bool ebav0780m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1920h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1921h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav2060s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3899s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0136h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c))))
//      public bool ebav0138h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0141h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c))))
//      public bool ebav0143h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c))))
//      public bool ebav0144h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0787m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0788m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0789m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0790m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0791m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0792m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0793m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0794m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c), iaf:numeric-unary-minus($d), $e, $f, iaf:numeric-unary-minus($g), $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, iaf:numeric-unary-minus($r), iaf:numeric-unary-minus($s), iaf:numeric-unary-minus($t), iaf:numeric-unary-minus($u), iaf:numeric-unary-minus($v), iaf:numeric-unary-minus($w), iaf:numeric-unary-minus($x), iaf:numeric-unary-minus($y), $z, $aa, $bb)))
//      public bool ebav0795m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_aa = parameters.FirstOrDefault(i => i.Name == "aa").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_bb = parameters.FirstOrDefault(i => i.Name == "bb").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
//         var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
//         var p_u = parameters.FirstOrDefault(i => i.Name == "u").DoubleValue;
//         var p_v = parameters.FirstOrDefault(i => i.Name == "v").DoubleValue;
//         var p_w = parameters.FirstOrDefault(i => i.Name == "w").DoubleValue;
//         var p_x = parameters.FirstOrDefault(i => i.Name == "x").DoubleValue;
//         var p_y = parameters.FirstOrDefault(i => i.Name == "y").DoubleValue;
//         var p_z = parameters.FirstOrDefault(i => i.Name == "z").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(p_d), p_e, p_f, functions.N_Unary_Minus(p_g), p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, functions.N_Unary_Minus(p_r), functions.N_Unary_Minus(p_s), functions.N_Unary_Minus(p_t), functions.N_Unary_Minus(p_u), functions.N_Unary_Minus(p_v), functions.N_Unary_Minus(p_w), functions.N_Unary_Minus(p_x), functions.N_Unary_Minus(p_y), p_z, p_aa, p_bb));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0798m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c), iaf:numeric-unary-minus($d), $e, $f, iaf:numeric-unary-minus($g), $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, iaf:numeric-unary-minus($r))))
//      public bool ebav1699m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(p_d), p_e, p_f, functions.N_Unary_Minus(p_g), p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, functions.N_Unary_Minus(p_r)));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3900s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0769m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav0770m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0771m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0772m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h)))
//      public bool ebav0773m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, iaf:sum($k))))
//      public bool ebav1698m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, functions.sum(p_k)));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3898s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum((iaf:sum($b), $c)))
//      public bool ebav0775m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(functions.sum(p_b), p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0785m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0777m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0856m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1703m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0783m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0784m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0786m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0799m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0800m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav1219m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1908h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3901s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0801m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0802m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0803m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0804m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav1220m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1909h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3902s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0805m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0806m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0807m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0808m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav1221m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1930h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3903s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3904s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav0809m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0810m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0811m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0812m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0813m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1931h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1932h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3905s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3906s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav0817m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0818m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0819m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0820m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0821m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav0822m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0823m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0824m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0825m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0826m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3917s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3918s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3919s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3920s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3921s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0827m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav0828m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1942h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1943h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1944h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1945h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1946h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1947h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1948h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3922s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3923s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3924s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0829m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0830m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0831m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0835m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0837m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3133m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3134m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3135m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3927s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0839m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0840m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0841m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3928s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3929s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0842m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0843m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0844m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0845m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0846m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0847m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0848m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0849m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0850m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0851m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0852m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0854m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1950h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1951h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3930s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3931s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0855m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1702m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0857m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0858m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0859m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0860m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav0862m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0863m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0864m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0865m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav0867m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0869m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1952h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1953h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1954h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1955h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1956h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3932s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3933s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0874m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0875m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav0876m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0877m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0878m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3912s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3913s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3914s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3915s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0879m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3132m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3142m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3916s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0880m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0881m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0882m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0884m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0885m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0886m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3953s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3954s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3955s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav0887m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0888m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1713m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3956s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0890m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0891m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0892m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0893m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav0894m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h)))
//      public bool ebav0895m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1959h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1960h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1961h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1962h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1963h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1964h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3936s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3937s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3938s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3939s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum(($a, $b)), $c)
//      public bool ebav0896m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1188m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1189m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1190m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1191m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1192m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1193m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1194m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1195m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1196m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1197m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1200m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1201m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1202m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1203m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k)))
//      public bool ebav1204m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1206m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1965h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1966h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1967h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3944s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3945s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3946s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3947s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0897m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0898m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0899m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0900m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0901m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0902m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0903m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0904m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0905m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0906m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0907m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0908m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav0909m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1714m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1715m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1716m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1717m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1718m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1719m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1720m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1721m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1722m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1723m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1724m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1725m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1726m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1727m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum(($a, $b)), $c)
//      public bool ebav0911m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1207m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1208m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1209m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1211m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1212m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1213m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1214m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1215m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k)))
//      public bool ebav1216m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1218m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1968h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1969h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1970h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3948s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3949s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, iaf:sum($b))
//      public bool ebav0912m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0914m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0915m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0916m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0917m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0918m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0919m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0920m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0921m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav0922m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0923m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3943s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0924m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0925m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0926m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0927m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0928m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0929m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0930m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0931m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0932m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0933m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0934m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0935m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0936m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0937m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0938m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0939m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0940m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0941m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0942m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0943m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0944m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0945m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0946m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0947m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav0948m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0949m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0950m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0951m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0952m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, $r, $s, $t)))
//      public bool ebav0953m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
//         var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, p_r, p_s, p_t));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1728m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1729m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1730m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1731m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1978h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1979h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1980h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3960s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav0954m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0955m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav0956m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m)))
//      public bool ebav0957m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3961s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0958m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0959m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0960m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0961m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0962m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0963m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0964m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0965m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0966m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0967m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0968m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0969m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0970m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0971m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0972m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0973m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0974m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0975m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0976m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0977m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0978m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal(iaf:sum(($a, $b)), $c)
//      public bool ebav0979m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a, p_b), p_c);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c), iaf:numeric-unary-minus($d), $e, $f, iaf:numeric-unary-minus($g), $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, iaf:numeric-unary-minus($r), iaf:numeric-unary-minus($s), iaf:numeric-unary-minus($t), iaf:numeric-unary-minus($u), iaf:numeric-unary-minus($v), iaf:numeric-unary-minus($w), iaf:numeric-unary-minus($x), iaf:numeric-unary-minus($y), $z, $aa, $bb)))
//      public bool ebav0980m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_aa = parameters.FirstOrDefault(i => i.Name == "aa").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_bb = parameters.FirstOrDefault(i => i.Name == "bb").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
//         var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
//         var p_u = parameters.FirstOrDefault(i => i.Name == "u").DoubleValue;
//         var p_v = parameters.FirstOrDefault(i => i.Name == "v").DoubleValue;
//         var p_w = parameters.FirstOrDefault(i => i.Name == "w").DoubleValue;
//         var p_x = parameters.FirstOrDefault(i => i.Name == "x").DoubleValue;
//         var p_y = parameters.FirstOrDefault(i => i.Name == "y").DoubleValue;
//         var p_z = parameters.FirstOrDefault(i => i.Name == "z").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(p_d), p_e, p_f, functions.N_Unary_Minus(p_g), p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, functions.N_Unary_Minus(p_r), functions.N_Unary_Minus(p_s), functions.N_Unary_Minus(p_t), functions.N_Unary_Minus(p_u), functions.N_Unary_Minus(p_v), functions.N_Unary_Minus(p_w), functions.N_Unary_Minus(p_x), functions.N_Unary_Minus(p_y), p_z, p_aa, p_bb));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c), iaf:numeric-unary-minus($d), $e, $f, iaf:numeric-unary-minus($g), $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, iaf:numeric-unary-minus($r))))
//      public bool ebav1732m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(p_d), p_e, p_f, functions.N_Unary_Minus(p_g), p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, functions.N_Unary_Minus(p_r)));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, iaf:numeric-unary-minus($c))))
//      public bool ebav1981h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav1982h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3962s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0985m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0986m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0987m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav0988m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3136m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3137m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3149m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3150m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3151m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3152m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3964s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0993m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav0994m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0995m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3967s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0996m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav0997m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //if ($a != 0) then ($b != 0) else (true())
//      public bool ebav0998m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return p_a!=0 ? p_b!=0 : true;
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3972s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav0999m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1001m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3141m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3154m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3155m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3973s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1003m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1004m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1005m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1006m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1007m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1008m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1009m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1010m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1014m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1015m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav3145m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1017m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1018m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1019m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1020m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1021m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1022m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1024m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //if (iaf:numeric-greater-than(iaf:sum($a), 0)) then (iaf:numeric-greater-than($b, 0)) else (true())
//      public bool ebav1025m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Greater(functions.sum(p_a), 0) ? functions.N_Greater(p_b, 0) : true;
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1992h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3974s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1026m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1028m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1029m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1027m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1030m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum((iaf:sum($b), iaf:numeric-unary-minus(iaf:sum($c)), iaf:numeric-unary-minus(iaf:sum($d)))))
//      public bool ebav1032m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValues;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(functions.sum(p_b), functions.N_Unary_Minus(functions.sum(p_c)), functions.N_Unary_Minus(functions.sum(p_d))));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1033m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1034m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum((iaf:sum($b), iaf:sum($c), iaf:sum($d))))
//      public bool ebav1036m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValues;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(functions.sum(p_b), functions.sum(p_c), functions.sum(p_d)));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1038m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1039m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1040m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3950s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3951s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1041m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1042m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1043m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1044m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1045m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1046m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1971h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1047m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1048m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav1985h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1986h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav1987h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3970s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum((iaf:sum($b), iaf:sum($c))))
//      public bool ebav1049m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(functions.sum(p_b), functions.sum(p_c)));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1050m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1051m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1052m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1053m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1054m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1055m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1056m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o)))
//      public bool ebav1058m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k)))
//      public bool ebav1059m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j)))
//      public bool ebav1060m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i)))
//      public bool ebav1061m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1062m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1063m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1064m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1065m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l)))
//      public bool ebav1066m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1067m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1225m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1749m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h)))
//      public bool ebav1750m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i)))
//      public bool ebav1751m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1752m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j)))
//      public bool ebav1754m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l)))
//      public bool ebav1756m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h)))
//      public bool ebav1757m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1758m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l)))
//      public bool ebav1759m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m)))
//      public bool ebav1760m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1761m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m)))
//      public bool ebav1762m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i)))
//      public bool ebav1763m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav1998h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav2027s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav2028s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav2062s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3990s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1068m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3940s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1069m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1070m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1071m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1072m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1073m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1074m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), iaf:sum($b))
//      public bool ebav1075m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1078m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1081m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1084m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than($a, iaf:sum($b))
//      public bool ebav1086m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_LessEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1087m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3941s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1088m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1093m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1094m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1095m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1996h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1997h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3978s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1096m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1097m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1098m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3979s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3980s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1099m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3982s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1103m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1983h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1984h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3969s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1104m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1105m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1106m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1107m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1108m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1109m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1110m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1111m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1112m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1113m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1114m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1115m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1116m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1764m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3983s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3984s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1117m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1118m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3985s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1121m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3986s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav3987s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1122m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1123m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum(($b, $c)))
//      public bool ebav3316h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1124m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1125m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1126m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1127m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n, $o, $p, $q, $r, $s, $t, $u)))
//      public bool ebav1128m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
//         var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
//         var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
//         var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
//         var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
//         var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
//         var p_u = parameters.FirstOrDefault(i => i.Name == "u").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, p_r, p_s, p_t, p_u));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1765m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1766m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1767m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1768m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1972h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1973h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1974h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1975h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1976h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3957s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1129m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3958s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1130m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1131m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1132m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g, $h, $i, $j, $k, $l, $m, $n)))
//      public bool ebav1133m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
//         var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
//         var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
//         var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
//         var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
//         var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
//         var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1134m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1135m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1769m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1770m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1977h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav2061s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3959s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1137m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1139m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1140m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3989s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1160m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3143m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3144m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, 1)
//      public bool ebav1241m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 1);
//      }

//       //iaf:numeric-less-equal-than($a, 1)
//      public bool ebav1242m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 1);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1995h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3976s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //$a = (xs:QName('eba_RP:x11'), xs:QName('eba_RP:x3'), xs:QName('eba_RP:x1'))
//      public bool ebav4027a(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
//         return $a=(functions.XS_QName("eba_RP:x11"), functions.XS_QName("eba_RP:x3"), functions.XS_QName("eba_RP:x1"));
//      }

//       //iaf:numeric-less-equal-than($a, 1)
//      public bool ebav1243m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 1);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3977s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1251m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1252m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1253m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1254m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1255m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1256m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1257m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1258m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1259m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1260m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1261m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1262m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1263m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1264m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1265m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1266m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1267m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1268m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1269m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1270m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1271m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1272m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1273m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1274m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1275m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1276m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1277m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1278m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1279m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1280m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1281m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1282m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1283m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1284m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1285m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1286m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1287m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1288m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1289m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1290m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1291m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1292m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1293m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1294m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1295m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1296m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1297m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1298m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1299m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1300m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1301m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1302m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1303m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1304m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1305m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1306m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1307m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1308m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1309m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1310m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1311m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav1312m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1329m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1330m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1331m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1332m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1333m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1334m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1335m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1925h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1926h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1927h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1928h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1337m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1338m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1339m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1340m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1341m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1342m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1343m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1344m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1345m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1346m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1347m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1348m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1349m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1350m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1351m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1352m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1353m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1354m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1355m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1356m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1357m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1358m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1359m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1360m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1361m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1362m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1363m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1364m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1365m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1366m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1367m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1368m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1369m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1370m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1371m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1372m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1373m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1374m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1375m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav1376m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav1383m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1384m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1385m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav1386m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1734m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1736m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav1738m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav3146m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav3147m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), $b)
//      public bool ebav3148m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1742m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1743m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1744m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1745m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1746m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1747m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal(iaf:sum($a), iaf:sum($b))
//      public bool ebav1748m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1911h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1912h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1913h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1914h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1915h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1916h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1917h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1918h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f)))
//      public bool ebav1919h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1922h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1923h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1924h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1933h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3907s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1935h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1936h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1937h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1938h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1939h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1940h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e, $f, $g)))
//      public bool ebav1941h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
//         var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1949h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3926s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d, $e)))
//      public bool ebav1988h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav1989h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav1990h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav1991h(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3971s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2701m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2702m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2703m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2706m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2707m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2709m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2712m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2715m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2718m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2721m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2724m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2727m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2730m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2733m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2736m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav2739m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2742m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2744m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2746m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2748m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2704m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2705m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2710m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2713m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2716m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2719m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2722m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2725m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2728m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2731m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2734m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2737m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav2740m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav2750m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav2751m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav2752m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav2753m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav4154m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav4155m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2711m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2714m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2717m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2720m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2723m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2726m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav2729m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2732m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2735m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav2738m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c, $d)))
//      public bool ebav2741m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2743m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2745m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2747m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav2749m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2781m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2782m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2783m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2784m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2785m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2786m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2787m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2788m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2789m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2790m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2791m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2792m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2793m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2794m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2795m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2796m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2797m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2798m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2799m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav2800m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav3018m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav3019m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav3020m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, $b)
//      public bool ebav3021m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_Equals(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav3078m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav3079m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, iaf:sum($b))
//      public bool ebav3080m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_GreaterEqual(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3081m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3082m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3083m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3084m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3085m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3086m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3089m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3090m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3092m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3095m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3098m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3101m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3104m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3107m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3110m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3113m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3115m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3117m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3119m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3087m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3088m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3093m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3096m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3099m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3102m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3105m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3108m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3111m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3121m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3122m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3123m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, $b)
//      public bool ebav3124m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_GreaterEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, 0)
//      public bool ebav4156m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_LessEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav4157m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3094m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3097m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3100m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3103m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3106m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3109m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-equal($a, iaf:sum(($b, $c)))
//      public bool ebav3112m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
//         return functions.N_Equals(p_a, functions.sum(p_b, p_c));
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3114m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3116m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3118m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3120m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3125m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3126m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3127m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3128m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3129m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav3130m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than(iaf:sum($a), $b)
//      public bool ebav3131m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValues;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(functions.sum(p_a), p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3138m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-less-equal-than($a, $b)
//      public bool ebav3139m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
//         return functions.N_LessEqual(p_a, p_b);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3965s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-equal($a, iaf:sum($b))
//      public bool ebav3153m(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValues;
//         return functions.N_Equals(p_a, functions.sum(p_b));
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3968s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3925s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3942s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3952s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3966s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3975s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3981s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //iaf:numeric-greater-equal-than($a, 0)
//      public bool ebav3988s(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
//         return functions.N_GreaterEqual(p_a, 0);
//      }

//       //if ($AccountingStandard = 'IFRS') then ($a = xs:QName('eba_AS:x2')) else (true())
//      public bool ebav4004c(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
//         var p_AccountingStandard = parameters.FirstOrDefault(i => i.Name == "AccountingStandard").StringValue;
//         return p_AccountingStandard='IFRS' ? $a=xs:QName("eba_AS:x2") : true;
//      }

//       //if ($AccountingStandard = 'GAAP') then ($a = xs:QName('eba_AS:x1')) else (true())
//      public bool ebav4005c(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
//         var p_AccountingStandard = parameters.FirstOrDefault(i => i.Name == "AccountingStandard").StringValue;
//         return p_AccountingStandard='GAAP' ? $a=xs:QName("eba_AS:x1") : true;
//      }

//       //$a = xs:QName('eba_SC:x7')
//      public bool ebav4006c(List<ValidationParameter> parameters)
//      {
//         var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
//         return $a=xs:QName("eba_SC:x7");
//      }


//  }
//}
