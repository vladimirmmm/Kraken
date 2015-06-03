using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicalModel.Expressions;
using LogicalModel.Base;
using LogicalModel.Validation;
using Utilities;
namespace LogicalModel.Validation
{
    public class ValidationsX : ValidationFunctionContainer
    {
        public ValidationsX()
        {
            this.FunctionDictionary.Add("ebav0002h", this.ebav0002h);
            this.FunctionDictionary.Add("ebav0006h", this.ebav0006h);
            this.FunctionDictionary.Add("ebav0569m", this.ebav0569m);
            this.FunctionDictionary.Add("ebav0571m", this.ebav0571m);
            this.FunctionDictionary.Add("ebav0572m", this.ebav0572m);
            this.FunctionDictionary.Add("ebav0574m", this.ebav0574m);
            this.FunctionDictionary.Add("ebav0578m", this.ebav0578m);
            this.FunctionDictionary.Add("ebav0579m", this.ebav0579m);
            this.FunctionDictionary.Add("ebav3763s", this.ebav3763s);
            this.FunctionDictionary.Add("ebav3764s", this.ebav3764s);
            this.FunctionDictionary.Add("ebav3765s", this.ebav3765s);
            this.FunctionDictionary.Add("ebav3766s", this.ebav3766s);
            this.FunctionDictionary.Add("ebav3767s", this.ebav3767s);
            this.FunctionDictionary.Add("ebav0004h", this.ebav0004h);
            this.FunctionDictionary.Add("ebav0619m", this.ebav0619m);
            this.FunctionDictionary.Add("ebav0620m", this.ebav0620m);
            this.FunctionDictionary.Add("ebav0622m", this.ebav0622m);
            this.FunctionDictionary.Add("ebav0623m", this.ebav0623m);
            this.FunctionDictionary.Add("ebav3778s", this.ebav3778s);
            this.FunctionDictionary.Add("ebav3779s", this.ebav3779s);
            this.FunctionDictionary.Add("ebav3780s", this.ebav3780s);
            this.FunctionDictionary.Add("ebav3781s", this.ebav3781s);
            this.FunctionDictionary.Add("ebav3782s", this.ebav3782s);
            this.FunctionDictionary.Add("ebav0005h", this.ebav0005h);
            this.FunctionDictionary.Add("ebav0630m", this.ebav0630m);
            this.FunctionDictionary.Add("ebav0631m", this.ebav0631m);
            this.FunctionDictionary.Add("ebav3789s", this.ebav3789s);
            this.FunctionDictionary.Add("ebav3790s", this.ebav3790s);
            this.FunctionDictionary.Add("ebav3791s", this.ebav3791s);
            this.FunctionDictionary.Add("ebav0007h", this.ebav0007h);
            this.FunctionDictionary.Add("ebav0625m", this.ebav0625m);
            this.FunctionDictionary.Add("ebav0626m", this.ebav0626m);
            this.FunctionDictionary.Add("ebav0627m", this.ebav0627m);
            this.FunctionDictionary.Add("ebav0628m", this.ebav0628m);
            this.FunctionDictionary.Add("ebav3783s", this.ebav3783s);
            this.FunctionDictionary.Add("ebav3784s", this.ebav3784s);
            this.FunctionDictionary.Add("ebav3785s", this.ebav3785s);
            this.FunctionDictionary.Add("ebav3786s", this.ebav3786s);
            this.FunctionDictionary.Add("ebav3787s", this.ebav3787s);
            this.FunctionDictionary.Add("ebav3788s", this.ebav3788s);
            this.FunctionDictionary.Add("ebav0009h", this.ebav0009h);
            this.FunctionDictionary.Add("ebav0505m", this.ebav0505m);
            this.FunctionDictionary.Add("ebav0506m", this.ebav0506m);
            this.FunctionDictionary.Add("ebav0507m", this.ebav0507m);
            this.FunctionDictionary.Add("ebav0510m", this.ebav0510m);
            this.FunctionDictionary.Add("ebav0512m", this.ebav0512m);
            this.FunctionDictionary.Add("ebav0513m", this.ebav0513m);
            this.FunctionDictionary.Add("ebav0516m", this.ebav0516m);
            this.FunctionDictionary.Add("ebav0517m", this.ebav0517m);
            this.FunctionDictionary.Add("ebav0518m", this.ebav0518m);
            this.FunctionDictionary.Add("ebav0519m", this.ebav0519m);
            this.FunctionDictionary.Add("ebav0520m", this.ebav0520m);
            this.FunctionDictionary.Add("ebav0521m", this.ebav0521m);
            this.FunctionDictionary.Add("ebav0522m", this.ebav0522m);
            this.FunctionDictionary.Add("ebav0523m", this.ebav0523m);
            this.FunctionDictionary.Add("ebav0524m", this.ebav0524m);
            this.FunctionDictionary.Add("ebav0525m", this.ebav0525m);
            this.FunctionDictionary.Add("ebav0526m", this.ebav0526m);
            this.FunctionDictionary.Add("ebav0527m", this.ebav0527m);
            this.FunctionDictionary.Add("ebav2052s", this.ebav2052s);
            this.FunctionDictionary.Add("ebav3736s", this.ebav3736s);
            this.FunctionDictionary.Add("ebav3737s", this.ebav3737s);
            this.FunctionDictionary.Add("ebav3738s", this.ebav3738s);
            this.FunctionDictionary.Add("ebav3739s", this.ebav3739s);
            this.FunctionDictionary.Add("ebav3740s", this.ebav3740s);
            this.FunctionDictionary.Add("ebav3741s", this.ebav3741s);
            this.FunctionDictionary.Add("ebav3742s", this.ebav3742s);
            this.FunctionDictionary.Add("ebav3743s", this.ebav3743s);
            this.FunctionDictionary.Add("ebav3744s", this.ebav3744s);
            this.FunctionDictionary.Add("ebav4047m", this.ebav4047m);
            this.FunctionDictionary.Add("ebav4048m", this.ebav4048m);
            this.FunctionDictionary.Add("ebav4049m", this.ebav4049m);
            this.FunctionDictionary.Add("ebav4050m", this.ebav4050m);
            this.FunctionDictionary.Add("ebav4051m", this.ebav4051m);
            this.FunctionDictionary.Add("ebav4052m", this.ebav4052m);
            this.FunctionDictionary.Add("ebav4053m", this.ebav4053m);
            this.FunctionDictionary.Add("ebav4054m", this.ebav4054m);
            this.FunctionDictionary.Add("ebav4055m", this.ebav4055m);
            this.FunctionDictionary.Add("ebav4056m", this.ebav4056m);
            this.FunctionDictionary.Add("ebav4057m", this.ebav4057m);
            this.FunctionDictionary.Add("ebav4058m", this.ebav4058m);
            this.FunctionDictionary.Add("ebav4059m", this.ebav4059m);
            this.FunctionDictionary.Add("ebav4060m", this.ebav4060m);
            this.FunctionDictionary.Add("ebav4061m", this.ebav4061m);
            this.FunctionDictionary.Add("ebav4062m", this.ebav4062m);
            this.FunctionDictionary.Add("ebav4063m", this.ebav4063m);
            this.FunctionDictionary.Add("ebav4064m", this.ebav4064m);
            this.FunctionDictionary.Add("ebav0010h", this.ebav0010h);
            this.FunctionDictionary.Add("ebav0305m", this.ebav0305m);
            this.FunctionDictionary.Add("ebav0306m", this.ebav0306m);
            this.FunctionDictionary.Add("ebav0307m", this.ebav0307m);
            this.FunctionDictionary.Add("ebav0308m", this.ebav0308m);
            this.FunctionDictionary.Add("ebav0310m", this.ebav0310m);
            this.FunctionDictionary.Add("ebav0311m", this.ebav0311m);
            this.FunctionDictionary.Add("ebav0312m", this.ebav0312m);
            this.FunctionDictionary.Add("ebav0313m", this.ebav0313m);
            this.FunctionDictionary.Add("ebav0314m", this.ebav0314m);
            this.FunctionDictionary.Add("ebav0315m", this.ebav0315m);
            this.FunctionDictionary.Add("ebav0316m", this.ebav0316m);
            this.FunctionDictionary.Add("ebav0317m", this.ebav0317m);
            this.FunctionDictionary.Add("ebav0318m", this.ebav0318m);
            this.FunctionDictionary.Add("ebav0319m", this.ebav0319m);
            this.FunctionDictionary.Add("ebav0320m", this.ebav0320m);
            this.FunctionDictionary.Add("ebav0321m", this.ebav0321m);
            this.FunctionDictionary.Add("ebav0322m", this.ebav0322m);
            this.FunctionDictionary.Add("ebav0323m", this.ebav0323m);
            this.FunctionDictionary.Add("ebav0324m", this.ebav0324m);
            this.FunctionDictionary.Add("ebav0325m", this.ebav0325m);
            this.FunctionDictionary.Add("ebav0326m", this.ebav0326m);
            this.FunctionDictionary.Add("ebav0327m", this.ebav0327m);
            this.FunctionDictionary.Add("ebav0328m", this.ebav0328m);
            this.FunctionDictionary.Add("ebav1644m", this.ebav1644m);
            this.FunctionDictionary.Add("ebav1645m", this.ebav1645m);
            this.FunctionDictionary.Add("ebav1647m", this.ebav1647m);
            this.FunctionDictionary.Add("ebav1648m", this.ebav1648m);
            this.FunctionDictionary.Add("ebav1649m", this.ebav1649m);
            this.FunctionDictionary.Add("ebav2037s", this.ebav2037s);
            this.FunctionDictionary.Add("ebav3697s", this.ebav3697s);
            this.FunctionDictionary.Add("ebav3698s", this.ebav3698s);
            this.FunctionDictionary.Add("ebav3699s", this.ebav3699s);
            this.FunctionDictionary.Add("ebav3700s", this.ebav3700s);
            this.FunctionDictionary.Add("ebav3701s", this.ebav3701s);
            this.FunctionDictionary.Add("ebav3702s", this.ebav3702s);
            this.FunctionDictionary.Add("ebav3703s", this.ebav3703s);
            this.FunctionDictionary.Add("ebav3704s", this.ebav3704s);
            this.FunctionDictionary.Add("ebav0011h", this.ebav0011h);
            this.FunctionDictionary.Add("ebav1659m", this.ebav1659m);
            this.FunctionDictionary.Add("ebav1784h", this.ebav1784h);
            this.FunctionDictionary.Add("ebav3707s", this.ebav3707s);
            this.FunctionDictionary.Add("ebav3708s", this.ebav3708s);
            this.FunctionDictionary.Add("ebav0012h", this.ebav0012h);
            this.FunctionDictionary.Add("ebav1660m", this.ebav1660m);
            this.FunctionDictionary.Add("ebav1661m", this.ebav1661m);
            this.FunctionDictionary.Add("ebav1785h", this.ebav1785h);
            this.FunctionDictionary.Add("ebav2040s", this.ebav2040s);
            this.FunctionDictionary.Add("ebav3709s", this.ebav3709s);
            this.FunctionDictionary.Add("ebav0022h", this.ebav0022h);
            this.FunctionDictionary.Add("ebav0023h", this.ebav0023h);
            this.FunctionDictionary.Add("ebav0277m", this.ebav0277m);
            this.FunctionDictionary.Add("ebav0278m", this.ebav0278m);
            this.FunctionDictionary.Add("ebav2036s", this.ebav2036s);
            this.FunctionDictionary.Add("ebav3694s", this.ebav3694s);
            this.FunctionDictionary.Add("ebav0101h", this.ebav0101h);
            this.FunctionDictionary.Add("ebav0102h", this.ebav0102h);
            this.FunctionDictionary.Add("ebav0132h", this.ebav0132h);
            this.FunctionDictionary.Add("ebav0657m", this.ebav0657m);
            this.FunctionDictionary.Add("ebav0658m", this.ebav0658m);
            this.FunctionDictionary.Add("ebav0659m", this.ebav0659m);
            this.FunctionDictionary.Add("ebav0660m", this.ebav0660m);
            this.FunctionDictionary.Add("ebav0661m", this.ebav0661m);
            this.FunctionDictionary.Add("ebav0662m", this.ebav0662m);
            this.FunctionDictionary.Add("ebav0663m", this.ebav0663m);
            this.FunctionDictionary.Add("ebav0664m", this.ebav0664m);
            this.FunctionDictionary.Add("ebav0665m", this.ebav0665m);
            this.FunctionDictionary.Add("ebav0666m", this.ebav0666m);
            this.FunctionDictionary.Add("ebav0667m", this.ebav0667m);
            this.FunctionDictionary.Add("ebav1688m", this.ebav1688m);
            this.FunctionDictionary.Add("ebav1689m", this.ebav1689m);
            this.FunctionDictionary.Add("ebav1691m", this.ebav1691m);
            this.FunctionDictionary.Add("ebav3803s", this.ebav3803s);
            this.FunctionDictionary.Add("ebav3804s", this.ebav3804s);
            this.FunctionDictionary.Add("ebav3805s", this.ebav3805s);
            this.FunctionDictionary.Add("ebav3806s", this.ebav3806s);
            this.FunctionDictionary.Add("ebav3807s", this.ebav3807s);
            this.FunctionDictionary.Add("ebav3808s", this.ebav3808s);
            this.FunctionDictionary.Add("ebav3809s", this.ebav3809s);
            this.FunctionDictionary.Add("ebav0108h", this.ebav0108h);
            this.FunctionDictionary.Add("ebav0128h", this.ebav0128h);
            this.FunctionDictionary.Add("ebav0224m", this.ebav0224m);
            this.FunctionDictionary.Add("ebav0225m", this.ebav0225m);
            this.FunctionDictionary.Add("ebav0226m", this.ebav0226m);
            this.FunctionDictionary.Add("ebav0227m", this.ebav0227m);
            this.FunctionDictionary.Add("ebav0228m", this.ebav0228m);
            this.FunctionDictionary.Add("ebav0229m", this.ebav0229m);
            this.FunctionDictionary.Add("ebav0230m", this.ebav0230m);
            this.FunctionDictionary.Add("ebav0231m", this.ebav0231m);
            this.FunctionDictionary.Add("ebav0232m", this.ebav0232m);
            this.FunctionDictionary.Add("ebav0233m", this.ebav0233m);
            this.FunctionDictionary.Add("ebav0234m", this.ebav0234m);
            this.FunctionDictionary.Add("ebav0235m", this.ebav0235m);
            this.FunctionDictionary.Add("ebav0236m", this.ebav0236m);
            this.FunctionDictionary.Add("ebav0237m", this.ebav0237m);
            this.FunctionDictionary.Add("ebav0238m", this.ebav0238m);
            this.FunctionDictionary.Add("ebav0239m", this.ebav0239m);
            this.FunctionDictionary.Add("ebav0240m", this.ebav0240m);
            this.FunctionDictionary.Add("ebav0241m", this.ebav0241m);
            this.FunctionDictionary.Add("ebav0242m", this.ebav0242m);
            this.FunctionDictionary.Add("ebav0243m", this.ebav0243m);
            this.FunctionDictionary.Add("ebav0244m", this.ebav0244m);
            this.FunctionDictionary.Add("ebav0245m", this.ebav0245m);
            this.FunctionDictionary.Add("ebav0246m", this.ebav0246m);
            this.FunctionDictionary.Add("ebav0247m", this.ebav0247m);
            this.FunctionDictionary.Add("ebav0248m", this.ebav0248m);
            this.FunctionDictionary.Add("ebav0249m", this.ebav0249m);
            this.FunctionDictionary.Add("ebav0250m", this.ebav0250m);
            this.FunctionDictionary.Add("ebav0251m", this.ebav0251m);
            this.FunctionDictionary.Add("ebav2034s", this.ebav2034s);
            this.FunctionDictionary.Add("ebav3688s", this.ebav3688s);
            this.FunctionDictionary.Add("ebav0148h", this.ebav0148h);
            this.FunctionDictionary.Add("ebav0172m", this.ebav0172m);
            this.FunctionDictionary.Add("ebav0173m", this.ebav0173m);
            this.FunctionDictionary.Add("ebav0174m", this.ebav0174m);
            this.FunctionDictionary.Add("ebav0175m", this.ebav0175m);
            this.FunctionDictionary.Add("ebav0176m", this.ebav0176m);
            this.FunctionDictionary.Add("ebav0177m", this.ebav0177m);
            this.FunctionDictionary.Add("ebav0178m", this.ebav0178m);
            this.FunctionDictionary.Add("ebav0179m", this.ebav0179m);
            this.FunctionDictionary.Add("ebav0180m", this.ebav0180m);
            this.FunctionDictionary.Add("ebav0181m", this.ebav0181m);
            this.FunctionDictionary.Add("ebav0182m", this.ebav0182m);
            this.FunctionDictionary.Add("ebav0183m", this.ebav0183m);
            this.FunctionDictionary.Add("ebav0184m", this.ebav0184m);
            this.FunctionDictionary.Add("ebav0185m", this.ebav0185m);
            this.FunctionDictionary.Add("ebav0186m", this.ebav0186m);
            this.FunctionDictionary.Add("ebav0187m", this.ebav0187m);
            this.FunctionDictionary.Add("ebav0188m", this.ebav0188m);
            this.FunctionDictionary.Add("ebav1771h", this.ebav1771h);
            this.FunctionDictionary.Add("ebav3685s", this.ebav3685s);
            this.FunctionDictionary.Add("ebav0150h", this.ebav0150h);
            this.FunctionDictionary.Add("ebav0204m", this.ebav0204m);
            this.FunctionDictionary.Add("ebav0205m", this.ebav0205m);
            this.FunctionDictionary.Add("ebav0206m", this.ebav0206m);
            this.FunctionDictionary.Add("ebav0207m", this.ebav0207m);
            this.FunctionDictionary.Add("ebav0208m", this.ebav0208m);
            this.FunctionDictionary.Add("ebav0209m", this.ebav0209m);
            this.FunctionDictionary.Add("ebav0210m", this.ebav0210m);
            this.FunctionDictionary.Add("ebav0211m", this.ebav0211m);
            this.FunctionDictionary.Add("ebav0212m", this.ebav0212m);
            this.FunctionDictionary.Add("ebav0213m", this.ebav0213m);
            this.FunctionDictionary.Add("ebav0214m", this.ebav0214m);
            this.FunctionDictionary.Add("ebav0215m", this.ebav0215m);
            this.FunctionDictionary.Add("ebav0216m", this.ebav0216m);
            this.FunctionDictionary.Add("ebav0217m", this.ebav0217m);
            this.FunctionDictionary.Add("ebav3686s", this.ebav3686s);
            this.FunctionDictionary.Add("ebav0190m", this.ebav0190m);
            this.FunctionDictionary.Add("ebav0197m", this.ebav0197m);
            this.FunctionDictionary.Add("ebav0201m", this.ebav0201m);
            this.FunctionDictionary.Add("ebav1772h", this.ebav1772h);
            this.FunctionDictionary.Add("ebav1773h", this.ebav1773h);
            this.FunctionDictionary.Add("ebav1774h", this.ebav1774h);
            this.FunctionDictionary.Add("ebav0218m", this.ebav0218m);
            this.FunctionDictionary.Add("ebav0219m", this.ebav0219m);
            this.FunctionDictionary.Add("ebav0220m", this.ebav0220m);
            this.FunctionDictionary.Add("ebav0221m", this.ebav0221m);
            this.FunctionDictionary.Add("ebav0222m", this.ebav0222m);
            this.FunctionDictionary.Add("ebav0223m", this.ebav0223m);
            this.FunctionDictionary.Add("ebav0252m", this.ebav0252m);
            this.FunctionDictionary.Add("ebav0253m", this.ebav0253m);
            this.FunctionDictionary.Add("ebav0255m", this.ebav0255m);
            this.FunctionDictionary.Add("ebav0256m", this.ebav0256m);
            this.FunctionDictionary.Add("ebav0257m", this.ebav0257m);
            this.FunctionDictionary.Add("ebav0258m", this.ebav0258m);
            this.FunctionDictionary.Add("ebav0259m", this.ebav0259m);
            this.FunctionDictionary.Add("ebav0260m", this.ebav0260m);
            this.FunctionDictionary.Add("ebav0261m", this.ebav0261m);
            this.FunctionDictionary.Add("ebav0262m", this.ebav0262m);
            this.FunctionDictionary.Add("ebav0264m", this.ebav0264m);
            this.FunctionDictionary.Add("ebav0265m", this.ebav0265m);
            this.FunctionDictionary.Add("ebav0266m", this.ebav0266m);
            this.FunctionDictionary.Add("ebav0267m", this.ebav0267m);
            this.FunctionDictionary.Add("ebav0268m", this.ebav0268m);
            this.FunctionDictionary.Add("ebav0269m", this.ebav0269m);
            this.FunctionDictionary.Add("ebav0270m", this.ebav0270m);
            this.FunctionDictionary.Add("ebav0271m", this.ebav0271m);
            this.FunctionDictionary.Add("ebav1628m", this.ebav1628m);
            this.FunctionDictionary.Add("ebav1629m", this.ebav1629m);
            this.FunctionDictionary.Add("ebav1630m", this.ebav1630m);
            this.FunctionDictionary.Add("ebav2000s", this.ebav2000s);
            this.FunctionDictionary.Add("ebav2035s", this.ebav2035s);
            this.FunctionDictionary.Add("ebav3689s", this.ebav3689s);
            this.FunctionDictionary.Add("ebav3690s", this.ebav3690s);
            this.FunctionDictionary.Add("ebav3691s", this.ebav3691s);
            this.FunctionDictionary.Add("ebav0274m", this.ebav0274m);
            this.FunctionDictionary.Add("ebav0275m", this.ebav0275m);
            this.FunctionDictionary.Add("ebav0276m", this.ebav0276m);
            this.FunctionDictionary.Add("ebav0309m", this.ebav0309m);
            this.FunctionDictionary.Add("ebav0332m", this.ebav0332m);
            this.FunctionDictionary.Add("ebav0335m", this.ebav0335m);
            this.FunctionDictionary.Add("ebav0336m", this.ebav0336m);
            this.FunctionDictionary.Add("ebav0338m", this.ebav0338m);
            this.FunctionDictionary.Add("ebav1662m", this.ebav1662m);
            this.FunctionDictionary.Add("ebav1663m", this.ebav1663m);
            this.FunctionDictionary.Add("ebav2041s", this.ebav2041s);
            this.FunctionDictionary.Add("ebav2042s", this.ebav2042s);
            this.FunctionDictionary.Add("ebav3710s", this.ebav3710s);
            this.FunctionDictionary.Add("ebav3711s", this.ebav3711s);
            this.FunctionDictionary.Add("ebav3712s", this.ebav3712s);
            this.FunctionDictionary.Add("ebav3713s", this.ebav3713s);
            this.FunctionDictionary.Add("ebav3714s", this.ebav3714s);
            this.FunctionDictionary.Add("ebav3715s", this.ebav3715s);
            this.FunctionDictionary.Add("ebav0334m", this.ebav0334m);
            this.FunctionDictionary.Add("ebav0342m", this.ebav0342m);
            this.FunctionDictionary.Add("ebav0343m", this.ebav0343m);
            this.FunctionDictionary.Add("ebav0344m", this.ebav0344m);
            this.FunctionDictionary.Add("ebav0345m", this.ebav0345m);
            this.FunctionDictionary.Add("ebav0346m", this.ebav0346m);
            this.FunctionDictionary.Add("ebav0347m", this.ebav0347m);
            this.FunctionDictionary.Add("ebav0348m", this.ebav0348m);
            this.FunctionDictionary.Add("ebav1665m", this.ebav1665m);
            this.FunctionDictionary.Add("ebav2049s", this.ebav2049s);
            this.FunctionDictionary.Add("ebav3721s", this.ebav3721s);
            this.FunctionDictionary.Add("ebav0350m", this.ebav0350m);
            this.FunctionDictionary.Add("ebav0351m", this.ebav0351m);
            this.FunctionDictionary.Add("ebav0352m", this.ebav0352m);
            this.FunctionDictionary.Add("ebav0353m", this.ebav0353m);
            this.FunctionDictionary.Add("ebav0354m", this.ebav0354m);
            this.FunctionDictionary.Add("ebav0355m", this.ebav0355m);
            this.FunctionDictionary.Add("ebav0356m", this.ebav0356m);
            this.FunctionDictionary.Add("ebav0357m", this.ebav0357m);
            this.FunctionDictionary.Add("ebav0358m", this.ebav0358m);
            this.FunctionDictionary.Add("ebav0359m", this.ebav0359m);
            this.FunctionDictionary.Add("ebav0360m", this.ebav0360m);
            this.FunctionDictionary.Add("ebav0361m", this.ebav0361m);
            this.FunctionDictionary.Add("ebav0362m", this.ebav0362m);
            this.FunctionDictionary.Add("ebav0363m", this.ebav0363m);
            this.FunctionDictionary.Add("ebav0364m", this.ebav0364m);
            this.FunctionDictionary.Add("ebav0365m", this.ebav0365m);
            this.FunctionDictionary.Add("ebav0366m", this.ebav0366m);
            this.FunctionDictionary.Add("ebav0367m", this.ebav0367m);
            this.FunctionDictionary.Add("ebav0368m", this.ebav0368m);
            this.FunctionDictionary.Add("ebav0369m", this.ebav0369m);
            this.FunctionDictionary.Add("ebav0370m", this.ebav0370m);
            this.FunctionDictionary.Add("ebav0371m", this.ebav0371m);
            this.FunctionDictionary.Add("ebav0372m", this.ebav0372m);
            this.FunctionDictionary.Add("ebav0373m", this.ebav0373m);
            this.FunctionDictionary.Add("ebav0374m", this.ebav0374m);
            this.FunctionDictionary.Add("ebav0375m", this.ebav0375m);
            this.FunctionDictionary.Add("ebav0376m", this.ebav0376m);
            this.FunctionDictionary.Add("ebav0377m", this.ebav0377m);
            this.FunctionDictionary.Add("ebav0378m", this.ebav0378m);
            this.FunctionDictionary.Add("ebav0379m", this.ebav0379m);
            this.FunctionDictionary.Add("ebav0380m", this.ebav0380m);
            this.FunctionDictionary.Add("ebav0381m", this.ebav0381m);
            this.FunctionDictionary.Add("ebav0382m", this.ebav0382m);
            this.FunctionDictionary.Add("ebav0383m", this.ebav0383m);
            this.FunctionDictionary.Add("ebav0384m", this.ebav0384m);
            this.FunctionDictionary.Add("ebav0385m", this.ebav0385m);
            this.FunctionDictionary.Add("ebav0386m", this.ebav0386m);
            this.FunctionDictionary.Add("ebav0387m", this.ebav0387m);
            this.FunctionDictionary.Add("ebav0388m", this.ebav0388m);
            this.FunctionDictionary.Add("ebav0389m", this.ebav0389m);
            this.FunctionDictionary.Add("ebav0390m", this.ebav0390m);
            this.FunctionDictionary.Add("ebav0391m", this.ebav0391m);
            this.FunctionDictionary.Add("ebav0392m", this.ebav0392m);
            this.FunctionDictionary.Add("ebav0393m", this.ebav0393m);
            this.FunctionDictionary.Add("ebav0394m", this.ebav0394m);
            this.FunctionDictionary.Add("ebav0395m", this.ebav0395m);
            this.FunctionDictionary.Add("ebav0396m", this.ebav0396m);
            this.FunctionDictionary.Add("ebav0397m", this.ebav0397m);
            this.FunctionDictionary.Add("ebav0398m", this.ebav0398m);
            this.FunctionDictionary.Add("ebav0399m", this.ebav0399m);
            this.FunctionDictionary.Add("ebav0400m", this.ebav0400m);
            this.FunctionDictionary.Add("ebav0401m", this.ebav0401m);
            this.FunctionDictionary.Add("ebav0402m", this.ebav0402m);
            this.FunctionDictionary.Add("ebav0403m", this.ebav0403m);
            this.FunctionDictionary.Add("ebav0404m", this.ebav0404m);
            this.FunctionDictionary.Add("ebav0405m", this.ebav0405m);
            this.FunctionDictionary.Add("ebav0406m", this.ebav0406m);
            this.FunctionDictionary.Add("ebav1666m", this.ebav1666m);
            this.FunctionDictionary.Add("ebav1667m", this.ebav1667m);
            this.FunctionDictionary.Add("ebav1668m", this.ebav1668m);
            this.FunctionDictionary.Add("ebav1669m", this.ebav1669m);
            this.FunctionDictionary.Add("ebav1670m", this.ebav1670m);
            this.FunctionDictionary.Add("ebav1671m", this.ebav1671m);
            this.FunctionDictionary.Add("ebav0407m", this.ebav0407m);
            this.FunctionDictionary.Add("ebav3722s", this.ebav3722s);
            this.FunctionDictionary.Add("ebav3723s", this.ebav3723s);
            this.FunctionDictionary.Add("ebav3724s", this.ebav3724s);
            this.FunctionDictionary.Add("ebav0411m", this.ebav0411m);
            this.FunctionDictionary.Add("ebav0412m", this.ebav0412m);
            this.FunctionDictionary.Add("ebav0413m", this.ebav0413m);
            this.FunctionDictionary.Add("ebav3725s", this.ebav3725s);
            this.FunctionDictionary.Add("ebav0415m", this.ebav0415m);
            this.FunctionDictionary.Add("ebav0416m", this.ebav0416m);
            this.FunctionDictionary.Add("ebav0417m", this.ebav0417m);
            this.FunctionDictionary.Add("ebav0418m", this.ebav0418m);
            this.FunctionDictionary.Add("ebav0420m", this.ebav0420m);
            this.FunctionDictionary.Add("ebav0421m", this.ebav0421m);
            this.FunctionDictionary.Add("ebav0422m", this.ebav0422m);
            this.FunctionDictionary.Add("ebav0423m", this.ebav0423m);
            this.FunctionDictionary.Add("ebav0425m", this.ebav0425m);
            this.FunctionDictionary.Add("ebav0426m", this.ebav0426m);
            this.FunctionDictionary.Add("ebav0427m", this.ebav0427m);
            this.FunctionDictionary.Add("ebav0428m", this.ebav0428m);
            this.FunctionDictionary.Add("ebav0430m", this.ebav0430m);
            this.FunctionDictionary.Add("ebav0431m", this.ebav0431m);
            this.FunctionDictionary.Add("ebav0432m", this.ebav0432m);
            this.FunctionDictionary.Add("ebav0433m", this.ebav0433m);
            this.FunctionDictionary.Add("ebav0435m", this.ebav0435m);
            this.FunctionDictionary.Add("ebav0436m", this.ebav0436m);
            this.FunctionDictionary.Add("ebav0437m", this.ebav0437m);
            this.FunctionDictionary.Add("ebav0438m", this.ebav0438m);
            this.FunctionDictionary.Add("ebav0440m", this.ebav0440m);
            this.FunctionDictionary.Add("ebav0441m", this.ebav0441m);
            this.FunctionDictionary.Add("ebav0442m", this.ebav0442m);
            this.FunctionDictionary.Add("ebav0443m", this.ebav0443m);
            this.FunctionDictionary.Add("ebav0445m", this.ebav0445m);
            this.FunctionDictionary.Add("ebav0446m", this.ebav0446m);
            this.FunctionDictionary.Add("ebav0447m", this.ebav0447m);
            this.FunctionDictionary.Add("ebav0448m", this.ebav0448m);
            this.FunctionDictionary.Add("ebav0450m", this.ebav0450m);
            this.FunctionDictionary.Add("ebav0451m", this.ebav0451m);
            this.FunctionDictionary.Add("ebav0452m", this.ebav0452m);
            this.FunctionDictionary.Add("ebav0453m", this.ebav0453m);
            this.FunctionDictionary.Add("ebav0455m", this.ebav0455m);
            this.FunctionDictionary.Add("ebav0456m", this.ebav0456m);
            this.FunctionDictionary.Add("ebav0457m", this.ebav0457m);
            this.FunctionDictionary.Add("ebav0458m", this.ebav0458m);
            this.FunctionDictionary.Add("ebav0460m", this.ebav0460m);
            this.FunctionDictionary.Add("ebav0461m", this.ebav0461m);
            this.FunctionDictionary.Add("ebav0462m", this.ebav0462m);
            this.FunctionDictionary.Add("ebav0463m", this.ebav0463m);
            this.FunctionDictionary.Add("ebav0465m", this.ebav0465m);
            this.FunctionDictionary.Add("ebav0466m", this.ebav0466m);
            this.FunctionDictionary.Add("ebav0467m", this.ebav0467m);
            this.FunctionDictionary.Add("ebav0468m", this.ebav0468m);
            this.FunctionDictionary.Add("ebav0470m", this.ebav0470m);
            this.FunctionDictionary.Add("ebav0471m", this.ebav0471m);
            this.FunctionDictionary.Add("ebav0472m", this.ebav0472m);
            this.FunctionDictionary.Add("ebav0473m", this.ebav0473m);
            this.FunctionDictionary.Add("ebav0475m", this.ebav0475m);
            this.FunctionDictionary.Add("ebav0476m", this.ebav0476m);
            this.FunctionDictionary.Add("ebav0477m", this.ebav0477m);
            this.FunctionDictionary.Add("ebav0478m", this.ebav0478m);
            this.FunctionDictionary.Add("ebav1672m", this.ebav1672m);
            this.FunctionDictionary.Add("ebav1673m", this.ebav1673m);
            this.FunctionDictionary.Add("ebav1674m", this.ebav1674m);
            this.FunctionDictionary.Add("ebav0480m", this.ebav0480m);
            this.FunctionDictionary.Add("ebav0481m", this.ebav0481m);
            this.FunctionDictionary.Add("ebav0483m", this.ebav0483m);
            this.FunctionDictionary.Add("ebav0484m", this.ebav0484m);
            this.FunctionDictionary.Add("ebav0485m", this.ebav0485m);
            this.FunctionDictionary.Add("ebav0486m", this.ebav0486m);
            this.FunctionDictionary.Add("ebav0487m", this.ebav0487m);
            this.FunctionDictionary.Add("ebav0488m", this.ebav0488m);
            this.FunctionDictionary.Add("ebav0489m", this.ebav0489m);
            this.FunctionDictionary.Add("ebav1617m", this.ebav1617m);
            this.FunctionDictionary.Add("ebav2051s", this.ebav2051s);
            this.FunctionDictionary.Add("ebav3729s", this.ebav3729s);
            this.FunctionDictionary.Add("ebav3730s", this.ebav3730s);
            this.FunctionDictionary.Add("ebav3731s", this.ebav3731s);
            this.FunctionDictionary.Add("ebav3732s", this.ebav3732s);
            this.FunctionDictionary.Add("ebav0482m", this.ebav0482m);
            this.FunctionDictionary.Add("ebav0493m", this.ebav0493m);
            this.FunctionDictionary.Add("ebav1675m", this.ebav1675m);
            this.FunctionDictionary.Add("ebav3733s", this.ebav3733s);
            this.FunctionDictionary.Add("ebav0494m", this.ebav0494m);
            this.FunctionDictionary.Add("ebav0495m", this.ebav0495m);
            this.FunctionDictionary.Add("ebav0496m", this.ebav0496m);
            this.FunctionDictionary.Add("ebav0497m", this.ebav0497m);
            this.FunctionDictionary.Add("ebav0498m", this.ebav0498m);
            this.FunctionDictionary.Add("ebav0499m", this.ebav0499m);
            this.FunctionDictionary.Add("ebav0501m", this.ebav0501m);
            this.FunctionDictionary.Add("ebav0503m", this.ebav0503m);
            this.FunctionDictionary.Add("ebav3734s", this.ebav3734s);
            this.FunctionDictionary.Add("ebav3735s", this.ebav3735s);
            this.FunctionDictionary.Add("ebav0500m", this.ebav0500m);
            this.FunctionDictionary.Add("ebav0502m", this.ebav0502m);
            this.FunctionDictionary.Add("ebav0511m", this.ebav0511m);
            this.FunctionDictionary.Add("ebav0529m", this.ebav0529m);
            this.FunctionDictionary.Add("ebav0530m", this.ebav0530m);
            this.FunctionDictionary.Add("ebav0531m", this.ebav0531m);
            this.FunctionDictionary.Add("ebav0532m", this.ebav0532m);
            this.FunctionDictionary.Add("ebav0533m", this.ebav0533m);
            this.FunctionDictionary.Add("ebav0534m", this.ebav0534m);
            this.FunctionDictionary.Add("ebav0536m", this.ebav0536m);
            this.FunctionDictionary.Add("ebav0537m", this.ebav0537m);
            this.FunctionDictionary.Add("ebav0538m", this.ebav0538m);
            this.FunctionDictionary.Add("ebav0540m", this.ebav0540m);
            this.FunctionDictionary.Add("ebav0541m", this.ebav0541m);
            this.FunctionDictionary.Add("ebav0542m", this.ebav0542m);
            this.FunctionDictionary.Add("ebav0543m", this.ebav0543m);
            this.FunctionDictionary.Add("ebav0544m", this.ebav0544m);
            this.FunctionDictionary.Add("ebav0545m", this.ebav0545m);
            this.FunctionDictionary.Add("ebav0546m", this.ebav0546m);
            this.FunctionDictionary.Add("ebav0547m", this.ebav0547m);
            this.FunctionDictionary.Add("ebav0548m", this.ebav0548m);
            this.FunctionDictionary.Add("ebav0549m", this.ebav0549m);
            this.FunctionDictionary.Add("ebav0550m", this.ebav0550m);
            this.FunctionDictionary.Add("ebav0551m", this.ebav0551m);
            this.FunctionDictionary.Add("ebav0552m", this.ebav0552m);
            this.FunctionDictionary.Add("ebav3745s", this.ebav3745s);
            this.FunctionDictionary.Add("ebav3746s", this.ebav3746s);
            this.FunctionDictionary.Add("ebav3747s", this.ebav3747s);
            this.FunctionDictionary.Add("ebav3748s", this.ebav3748s);
            this.FunctionDictionary.Add("ebav3749s", this.ebav3749s);
            this.FunctionDictionary.Add("ebav3750s", this.ebav3750s);
            this.FunctionDictionary.Add("ebav3751s", this.ebav3751s);
            this.FunctionDictionary.Add("ebav3752s", this.ebav3752s);
            this.FunctionDictionary.Add("ebav3753s", this.ebav3753s);
            this.FunctionDictionary.Add("ebav3754s", this.ebav3754s);
            this.FunctionDictionary.Add("ebav3755s", this.ebav3755s);
            this.FunctionDictionary.Add("ebav4065m", this.ebav4065m);
            this.FunctionDictionary.Add("ebav4066m", this.ebav4066m);
            this.FunctionDictionary.Add("ebav4067m", this.ebav4067m);
            this.FunctionDictionary.Add("ebav4068m", this.ebav4068m);
            this.FunctionDictionary.Add("ebav4069m", this.ebav4069m);
            this.FunctionDictionary.Add("ebav4070m", this.ebav4070m);
            this.FunctionDictionary.Add("ebav4071m", this.ebav4071m);
            this.FunctionDictionary.Add("ebav4072m", this.ebav4072m);
            this.FunctionDictionary.Add("ebav4073m", this.ebav4073m);
            this.FunctionDictionary.Add("ebav4074m", this.ebav4074m);
            this.FunctionDictionary.Add("ebav4075m", this.ebav4075m);
            this.FunctionDictionary.Add("ebav4078m", this.ebav4078m);
            this.FunctionDictionary.Add("ebav4079m", this.ebav4079m);
            this.FunctionDictionary.Add("ebav4080m", this.ebav4080m);
            this.FunctionDictionary.Add("ebav4081m", this.ebav4081m);
            this.FunctionDictionary.Add("ebav4082m", this.ebav4082m);
            this.FunctionDictionary.Add("ebav4083m", this.ebav4083m);
            this.FunctionDictionary.Add("ebav4084m", this.ebav4084m);
            this.FunctionDictionary.Add("ebav4085m", this.ebav4085m);
            this.FunctionDictionary.Add("ebav4086m", this.ebav4086m);
            this.FunctionDictionary.Add("ebav4087m", this.ebav4087m);
            this.FunctionDictionary.Add("ebav4088m", this.ebav4088m);
            this.FunctionDictionary.Add("ebav4089m", this.ebav4089m);
            this.FunctionDictionary.Add("ebav4090m", this.ebav4090m);
            this.FunctionDictionary.Add("ebav4091m", this.ebav4091m);
            this.FunctionDictionary.Add("ebav4092m", this.ebav4092m);
            this.FunctionDictionary.Add("ebav4093m", this.ebav4093m);
            this.FunctionDictionary.Add("ebav4094m", this.ebav4094m);
            this.FunctionDictionary.Add("ebav4153m", this.ebav4153m);
            this.FunctionDictionary.Add("ebav0535m", this.ebav0535m);
            this.FunctionDictionary.Add("ebav0539m", this.ebav0539m);
            this.FunctionDictionary.Add("ebav0553m", this.ebav0553m);
            this.FunctionDictionary.Add("ebav2054s", this.ebav2054s);
            this.FunctionDictionary.Add("ebav3756s", this.ebav3756s);
            this.FunctionDictionary.Add("ebav4007a", this.ebav4007a);
            this.FunctionDictionary.Add("ebav4010a", this.ebav4010a);
            this.FunctionDictionary.Add("ebav4011a", this.ebav4011a);
            this.FunctionDictionary.Add("ebav4014a", this.ebav4014a);
            this.FunctionDictionary.Add("ebav4015a", this.ebav4015a);
            this.FunctionDictionary.Add("ebav4018a", this.ebav4018a);
            this.FunctionDictionary.Add("ebav4020a", this.ebav4020a);
            this.FunctionDictionary.Add("ebav4021a", this.ebav4021a);
            this.FunctionDictionary.Add("ebav4022a", this.ebav4022a);
            this.FunctionDictionary.Add("ebav4024a", this.ebav4024a);
            this.FunctionDictionary.Add("ebav0554m", this.ebav0554m);
            this.FunctionDictionary.Add("ebav0555m", this.ebav0555m);
            this.FunctionDictionary.Add("ebav0556m", this.ebav0556m);
            this.FunctionDictionary.Add("ebav3757s", this.ebav3757s);
            this.FunctionDictionary.Add("ebav0558m", this.ebav0558m);
            this.FunctionDictionary.Add("ebav0560m", this.ebav0560m);
            this.FunctionDictionary.Add("ebav0563m", this.ebav0563m);
            this.FunctionDictionary.Add("ebav1141m", this.ebav1141m);
            this.FunctionDictionary.Add("ebav3758s", this.ebav3758s);
            this.FunctionDictionary.Add("ebav3759s", this.ebav3759s);
            this.FunctionDictionary.Add("ebav0562m", this.ebav0562m);
            this.FunctionDictionary.Add("ebav1142m", this.ebav1142m);
            this.FunctionDictionary.Add("ebav1143m", this.ebav1143m);
            this.FunctionDictionary.Add("ebav1144m", this.ebav1144m);
            this.FunctionDictionary.Add("ebav2055s", this.ebav2055s);
            this.FunctionDictionary.Add("ebav3760s", this.ebav3760s);
            this.FunctionDictionary.Add("ebav0568m", this.ebav0568m);
            this.FunctionDictionary.Add("ebav3762s", this.ebav3762s);
            this.FunctionDictionary.Add("ebav0581m", this.ebav0581m);
            this.FunctionDictionary.Add("ebav0582m", this.ebav0582m);
            this.FunctionDictionary.Add("ebav0583m", this.ebav0583m);
            this.FunctionDictionary.Add("ebav0589m", this.ebav0589m);
            this.FunctionDictionary.Add("ebav0590m", this.ebav0590m);
            this.FunctionDictionary.Add("ebav0592m", this.ebav0592m);
            this.FunctionDictionary.Add("ebav0593m", this.ebav0593m);
            this.FunctionDictionary.Add("ebav0594m", this.ebav0594m);
            this.FunctionDictionary.Add("ebav0596m", this.ebav0596m);
            this.FunctionDictionary.Add("ebav0597m", this.ebav0597m);
            this.FunctionDictionary.Add("ebav0598m", this.ebav0598m);
            this.FunctionDictionary.Add("ebav2056s", this.ebav2056s);
            this.FunctionDictionary.Add("ebav3768s", this.ebav3768s);
            this.FunctionDictionary.Add("ebav3769s", this.ebav3769s);
            this.FunctionDictionary.Add("ebav3770s", this.ebav3770s);
            this.FunctionDictionary.Add("ebav3771s", this.ebav3771s);
            this.FunctionDictionary.Add("ebav3772s", this.ebav3772s);
            this.FunctionDictionary.Add("ebav3773s", this.ebav3773s);
            this.FunctionDictionary.Add("ebav4031m", this.ebav4031m);
            this.FunctionDictionary.Add("ebav4032m", this.ebav4032m);
            this.FunctionDictionary.Add("ebav4033m", this.ebav4033m);
            this.FunctionDictionary.Add("ebav4034m", this.ebav4034m);
            this.FunctionDictionary.Add("ebav4035m", this.ebav4035m);
            this.FunctionDictionary.Add("ebav4036m", this.ebav4036m);
            this.FunctionDictionary.Add("ebav4037m", this.ebav4037m);
            this.FunctionDictionary.Add("ebav4038m", this.ebav4038m);
            this.FunctionDictionary.Add("ebav4039m", this.ebav4039m);
            this.FunctionDictionary.Add("ebav4040m", this.ebav4040m);
            this.FunctionDictionary.Add("ebav4041m", this.ebav4041m);
            this.FunctionDictionary.Add("ebav4042m", this.ebav4042m);
            this.FunctionDictionary.Add("ebav4043m", this.ebav4043m);
            this.FunctionDictionary.Add("ebav4044m", this.ebav4044m);
            this.FunctionDictionary.Add("ebav4045m", this.ebav4045m);
            this.FunctionDictionary.Add("ebav4046m", this.ebav4046m);
            this.FunctionDictionary.Add("ebav0600m", this.ebav0600m);
            this.FunctionDictionary.Add("ebav0602m", this.ebav0602m);
            this.FunctionDictionary.Add("ebav0604m", this.ebav0604m);
            this.FunctionDictionary.Add("ebav0605m", this.ebav0605m);
            this.FunctionDictionary.Add("ebav0606m", this.ebav0606m);
            this.FunctionDictionary.Add("ebav0607m", this.ebav0607m);
            this.FunctionDictionary.Add("ebav0608m", this.ebav0608m);
            this.FunctionDictionary.Add("ebav0609m", this.ebav0609m);
            this.FunctionDictionary.Add("ebav0610m", this.ebav0610m);
            this.FunctionDictionary.Add("ebav0611m", this.ebav0611m);
            this.FunctionDictionary.Add("ebav0612m", this.ebav0612m);
            this.FunctionDictionary.Add("ebav0613m", this.ebav0613m);
            this.FunctionDictionary.Add("ebav0614m", this.ebav0614m);
            this.FunctionDictionary.Add("ebav0615m", this.ebav0615m);
            this.FunctionDictionary.Add("ebav0616m", this.ebav0616m);
            this.FunctionDictionary.Add("ebav0617m", this.ebav0617m);
            this.FunctionDictionary.Add("ebav2057s", this.ebav2057s);
            this.FunctionDictionary.Add("ebav3774s", this.ebav3774s);
            this.FunctionDictionary.Add("ebav3775s", this.ebav3775s);
            this.FunctionDictionary.Add("ebav3776s", this.ebav3776s);
            this.FunctionDictionary.Add("ebav3777s", this.ebav3777s);
            this.FunctionDictionary.Add("ebav4095m", this.ebav4095m);
            this.FunctionDictionary.Add("ebav4096m", this.ebav4096m);
            this.FunctionDictionary.Add("ebav4097m", this.ebav4097m);
            this.FunctionDictionary.Add("ebav4098m", this.ebav4098m);
            this.FunctionDictionary.Add("ebav0629m", this.ebav0629m);
            this.FunctionDictionary.Add("ebav0635m", this.ebav0635m);
            this.FunctionDictionary.Add("ebav0636m", this.ebav0636m);
            this.FunctionDictionary.Add("ebav0637m", this.ebav0637m);
            this.FunctionDictionary.Add("ebav1906h", this.ebav1906h);
            this.FunctionDictionary.Add("ebav1907h", this.ebav1907h);
            this.FunctionDictionary.Add("ebav3792s", this.ebav3792s);
            this.FunctionDictionary.Add("ebav3793s", this.ebav3793s);
            this.FunctionDictionary.Add("ebav0639m", this.ebav0639m);
            this.FunctionDictionary.Add("ebav0641m", this.ebav0641m);
            this.FunctionDictionary.Add("ebav0642m", this.ebav0642m);
            this.FunctionDictionary.Add("ebav3794s", this.ebav3794s);
            this.FunctionDictionary.Add("ebav3795s", this.ebav3795s);
            this.FunctionDictionary.Add("ebav3796s", this.ebav3796s);
            this.FunctionDictionary.Add("ebav3797s", this.ebav3797s);
            this.FunctionDictionary.Add("ebav0640m", this.ebav0640m);
            this.FunctionDictionary.Add("ebav0643m", this.ebav0643m);
            this.FunctionDictionary.Add("ebav0644m", this.ebav0644m);
            this.FunctionDictionary.Add("ebav0645m", this.ebav0645m);
            this.FunctionDictionary.Add("ebav0668m", this.ebav0668m);
            this.FunctionDictionary.Add("ebav3810s", this.ebav3810s);
            this.FunctionDictionary.Add("ebav3811s", this.ebav3811s);
            this.FunctionDictionary.Add("ebav0670m", this.ebav0670m);
            this.FunctionDictionary.Add("ebav0672m", this.ebav0672m);
            this.FunctionDictionary.Add("ebav0673m", this.ebav0673m);
            this.FunctionDictionary.Add("ebav0674m", this.ebav0674m);
            this.FunctionDictionary.Add("ebav0675m", this.ebav0675m);
            this.FunctionDictionary.Add("ebav0677m", this.ebav0677m);
            this.FunctionDictionary.Add("ebav0678m", this.ebav0678m);
            this.FunctionDictionary.Add("ebav0683m", this.ebav0683m);
            this.FunctionDictionary.Add("ebav0684m", this.ebav0684m);
            this.FunctionDictionary.Add("ebav3812s", this.ebav3812s);
            this.FunctionDictionary.Add("ebav0700m", this.ebav0700m);
            this.FunctionDictionary.Add("ebav0701m", this.ebav0701m);
            this.FunctionDictionary.Add("ebav0702m", this.ebav0702m);
            this.FunctionDictionary.Add("ebav0703m", this.ebav0703m);
            this.FunctionDictionary.Add("ebav0704m", this.ebav0704m);
            this.FunctionDictionary.Add("ebav0705m", this.ebav0705m);
            this.FunctionDictionary.Add("ebav0706m", this.ebav0706m);
            this.FunctionDictionary.Add("ebav0707m", this.ebav0707m);
            this.FunctionDictionary.Add("ebav3813s", this.ebav3813s);
            this.FunctionDictionary.Add("ebav0709m", this.ebav0709m);
            this.FunctionDictionary.Add("ebav0710m", this.ebav0710m);
            this.FunctionDictionary.Add("ebav0712m", this.ebav0712m);
            this.FunctionDictionary.Add("ebav0713m", this.ebav0713m);
            this.FunctionDictionary.Add("ebav0714m", this.ebav0714m);
            this.FunctionDictionary.Add("ebav0715m", this.ebav0715m);
            this.FunctionDictionary.Add("ebav0716m", this.ebav0716m);
            this.FunctionDictionary.Add("ebav0717m", this.ebav0717m);
            this.FunctionDictionary.Add("ebav0718m", this.ebav0718m);
            this.FunctionDictionary.Add("ebav0719m", this.ebav0719m);
            this.FunctionDictionary.Add("ebav3814s", this.ebav3814s);
            this.FunctionDictionary.Add("ebav0721m", this.ebav0721m);
            this.FunctionDictionary.Add("ebav0722m", this.ebav0722m);
            this.FunctionDictionary.Add("ebav0726m", this.ebav0726m);
            this.FunctionDictionary.Add("ebav0727m", this.ebav0727m);
            this.FunctionDictionary.Add("ebav0728m", this.ebav0728m);
            this.FunctionDictionary.Add("ebav0730m", this.ebav0730m);
            this.FunctionDictionary.Add("ebav3815s", this.ebav3815s);
            this.FunctionDictionary.Add("ebav1156m", this.ebav1156m);
            this.FunctionDictionary.Add("ebav1157m", this.ebav1157m);
            this.FunctionDictionary.Add("ebav3761s", this.ebav3761s);
            this.FunctionDictionary.Add("ebav3687s", this.ebav3687s);
            this.FunctionDictionary.Add("ebav3706s", this.ebav3706s);
            this.FunctionDictionary.Add("ebav3716s", this.ebav3716s);
            this.FunctionDictionary.Add("ebav3717s", this.ebav3717s);
            this.FunctionDictionary.Add("ebav3718s", this.ebav3718s);
            this.FunctionDictionary.Add("ebav3719s", this.ebav3719s);
            this.FunctionDictionary.Add("ebav3720s", this.ebav3720s);
            this.FunctionDictionary.Add("ebav3726s", this.ebav3726s);
            this.FunctionDictionary.Add("ebav3727s", this.ebav3727s);
            this.FunctionDictionary.Add("ebav3728s", this.ebav3728s);
            this.FunctionDictionary.Add("ebav4002c", this.ebav4002c);
            this.FunctionDictionary.Add("ebav4003c", this.ebav4003c);
            this.FunctionDictionary.Add("ebav4008a", this.ebav4008a);
            this.FunctionDictionary.Add("ebav4009a", this.ebav4009a);
            this.FunctionDictionary.Add("ebav4016a", this.ebav4016a);
            this.FunctionDictionary.Add("ebav4029a", this.ebav4029a);

        }
        public bool ebav0002h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0006h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0569m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0571m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0572m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0574m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0578m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 0.08), p_b);
        }

        public bool ebav0579m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 0.12), p_b);
        }

        public bool ebav3763s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3764s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3765s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3766s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3767s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0004h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0619m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0620m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0622m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0623m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 0.08), p_b);
        }

        public bool ebav3778s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3779s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3780s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3781s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3782s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0005h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0630m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0631m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav3789s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3790s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3791s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0007h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0625m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0626m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0627m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0628m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav3783s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3784s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3785s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3786s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3787s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3788s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0009h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0505m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0506m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0507m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0510m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0512m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0513m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0516m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0517m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0518m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0519m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0520m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0521m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0522m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0523m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0524m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0525m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0526m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0527m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(functions.abs(p_a), functions.abs(p_b));
        }

        public bool ebav2052s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3736s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3737s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3738s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3739s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3740s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3741s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3742s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3743s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3744s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4047m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4048m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4049m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4050m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4051m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4052m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4053m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4054m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4055m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4056m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4057m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4058m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4059m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4060m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4061m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4062m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4063m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4064m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav0010h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0305m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0306m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0307m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0308m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(functions.N_Multiply(0.8, p_d)), functions.N_Unary_Minus(functions.N_Multiply(0.5, p_e))));
        }

        public bool ebav0310m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0311m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0312m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p));
        }

        public bool ebav0313m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o));
        }

        public bool ebav0314m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0315m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0316m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, 0);
        }

        public bool ebav0317m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, 0);
        }

        public bool ebav0318m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.02));
        }

        public bool ebav0319m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.1));
        }

        public bool ebav0320m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.2));
        }

        public bool ebav0321m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.35));
        }

        public bool ebav0322m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.5));
        }

        public bool ebav0323m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.7));
        }

        public bool ebav0324m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.75));
        }

        public bool ebav0325m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0326m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 1.5));
        }

        public bool ebav0327m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 2.5));
        }

        public bool ebav0328m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 12.5));
        }

        public bool ebav1644m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1645m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1647m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1648m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1649m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav2037s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3697s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3698s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3699s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3700s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3701s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3702s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3703s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3704s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0011h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1659m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(functions.N_Multiply(0.8, p_d)), functions.N_Unary_Minus(functions.N_Multiply(0.5, p_e))));
        }

        public bool ebav1784h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav3707s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3708s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav0012h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1660m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1661m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(functions.N_Multiply(0.8, p_d)), functions.N_Unary_Minus(functions.N_Multiply(0.5, p_e))));
        }

        public bool ebav1785h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav2040s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3709s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0022h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0023h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0277m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0278m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav2036s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3694s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0101h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0102h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0132h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0657m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0658m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0659m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0660m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0661m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0662m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0663m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0664m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0665m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0666m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0667m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.sum(p_b));
        }

        public bool ebav1688m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.N_Multiply(p_b, 0.1));
        }

        public bool ebav1689m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.N_Multiply(p_b, 0.1));
        }

        public bool ebav1691m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav3803s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3804s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3805s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3806s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3807s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3808s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3809s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0108h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0128h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0224m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0225m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
        }

        public bool ebav0226m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0227m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0228m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0229m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0230m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0231m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0232m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0233m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0234m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0235m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0236m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0237m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0238m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0239m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0240m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0241m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0242m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0243m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0244m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0245m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0246m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0247m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0248m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0249m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0250m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0251m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c)));
        }

        public bool ebav2034s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3688s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0148h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0172m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_aa = parameters.FirstOrDefault(i => i.Name == "aa").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_bb = parameters.FirstOrDefault(i => i.Name == "bb").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_cc = parameters.FirstOrDefault(i => i.Name == "cc").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
            var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
            var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
            var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
            var p_u = parameters.FirstOrDefault(i => i.Name == "u").DoubleValue;
            var p_v = parameters.FirstOrDefault(i => i.Name == "v").DoubleValue;
            var p_w = parameters.FirstOrDefault(i => i.Name == "w").DoubleValue;
            var p_x = parameters.FirstOrDefault(i => i.Name == "x").DoubleValue;
            var p_y = parameters.FirstOrDefault(i => i.Name == "y").DoubleValue;
            var p_z = parameters.FirstOrDefault(i => i.Name == "z").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q, p_r, p_s, p_t, p_u, p_v, p_w, p_x, p_y, p_z, p_aa, p_bb, p_cc));
        }

        public bool ebav0173m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0174m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0175m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0176m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0177m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0178m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0179m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0180m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0181m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Unary_Minus(p_b));
        }

        public bool ebav0182m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m));
        }

        public bool ebav0183m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0184m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0185m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Unary_Minus(p_b));
        }

        public bool ebav0186m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n));
        }

        public bool ebav0187m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0188m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav1771h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav3685s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav0150h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0204m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i));
        }

        public bool ebav0205m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0206m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0207m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q));
        }

        public bool ebav0208m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0209m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0210m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0211m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k));
        }

        public bool ebav0212m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0213m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0214m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0215m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0216m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0217m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav3686s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0190m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0197m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0201m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav1772h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1773h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1774h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0218m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, p_c));
        }

        public bool ebav0219m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(functions.N_Multiply(p_c, 0.045))));
        }

        public bool ebav0220m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, p_c));
        }

        public bool ebav0221m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(functions.N_Multiply(p_c, 0.06))));
        }

        public bool ebav0222m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, p_c));
        }

        public bool ebav0223m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(functions.N_Multiply(p_c, 0.08))));
        }

        public bool ebav0252m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0253m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e));
        }

        public bool ebav0255m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0256m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0257m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0258m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0259m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
        }

        public bool ebav0260m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0261m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0262m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0264m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0265m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0266m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l));
        }

        public bool ebav0267m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0268m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0269m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g));
        }

        public bool ebav0270m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0271m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1628m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1629m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav1630m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav2000s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav2035s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3689s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3690s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3691s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0274m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0275m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0276m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0309m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0332m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0335m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0336m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0338m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav1662m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav1663m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav2041s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav2042s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3710s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3711s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3712s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3713s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3714s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3715s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0334m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0342m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0343m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0344m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0345m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0346m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0347m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0348m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav1665m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav2049s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3721s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0350m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0351m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0352m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0353m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0354m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0355m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0356m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0357m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0358m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0359m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0360m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0361m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0362m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0363m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0364m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0365m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0366m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0367m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0368m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0369m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0370m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0371m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0372m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0373m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0374m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0375m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0376m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0377m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0378m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0379m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0380m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0381m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0382m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0383m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0384m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0385m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0386m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0387m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0388m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0389m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0390m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0391m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0392m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0393m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0394m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0395m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0396m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0397m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0398m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0399m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0400m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0401m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0402m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0403m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0404m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0405m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0406m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1666m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1667m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1668m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1669m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1670m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1671m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0407m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav3722s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3723s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3724s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0411m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0412m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0413m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav3725s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0415m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0416m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0417m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0418m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0420m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0421m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0422m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0423m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0425m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0426m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0427m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0428m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0430m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0431m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0432m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0433m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0435m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0436m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0437m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0438m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0440m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0441m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0442m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0443m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0445m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0446m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0447m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0448m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0450m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0451m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0452m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0453m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0455m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0456m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0457m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0458m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0460m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0461m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0462m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0463m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0465m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0466m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0467m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0468m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0470m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0471m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0472m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0473m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0475m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0476m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0477m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0478m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1672m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav1673m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav1674m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), p_b);
        }

        public bool ebav0480m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0481m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0.65);
        }

        public bool ebav0483m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0484m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 1.9));
        }

        public bool ebav0485m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.008));
        }

        public bool ebav0486m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 2.9));
        }

        public bool ebav0487m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.008));
        }

        public bool ebav0488m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 3.7));
        }

        public bool ebav0489m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.024));
        }

        public bool ebav1617m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 1);
        }

        public bool ebav2051s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3729s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3730s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3731s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3732s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0482m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0493m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0.65);
        }

        public bool ebav1675m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 1);
        }

        public bool ebav3733s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0494m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0));
        }

        public bool ebav0495m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 1));
        }

        public bool ebav0496m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.5));
        }

        public bool ebav0497m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.75));
        }

        public bool ebav0498m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 0.08));
        }

        public bool ebav0499m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 12.5));
        }

        public bool ebav0501m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0503m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav3734s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3735s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0500m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0502m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0511m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0529m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0530m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0531m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0532m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p, p_q));
        }

        public bool ebav0533m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n, p_o, p_p));
        }

        public bool ebav0534m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0536m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0537m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0538m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m));
        }

        public bool ebav0540m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0541m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0542m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0543m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0544m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0545m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0546m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0547m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0548m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0549m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0550m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0551m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0552m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(functions.abs(p_a), functions.abs(p_b));
        }

        public bool ebav3745s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3746s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3747s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3748s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3749s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3750s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3751s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3752s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3753s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3754s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3755s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4065m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4066m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4067m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4068m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4069m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4070m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4071m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4072m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4073m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4074m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4075m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4078m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4079m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4080m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4081m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4082m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4083m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4084m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4085m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4086m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4087m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4088m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4089m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4090m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4091m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4092m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4093m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4094m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g), functions.N_Multiply(p_h, p_i), functions.N_Multiply(p_j, p_k)), p_l));
        }

        public bool ebav4153m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j, p_k, p_l, p_m, p_n));
        }

        public bool ebav0535m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0539m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0553m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a, p_b, p_c), functions.sum(p_d, p_e, p_f, p_g));
        }

        public bool ebav2054s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3756s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4007a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_AP:x27"), functions.XS_QName("eba_AP:x42"), functions.XS_QName("eba_AP:x45"));
        }

        public bool ebav4010a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_ZZ:x6"), functions.XS_QName("eba_ZZ:x7"), functions.XS_QName("eba_ZZ:x8"), functions.XS_QName("eba_ZZ:x9"), functions.XS_QName("eba_ZZ:x10"), functions.XS_QName("eba_ZZ:x11"), functions.XS_QName("eba_ZZ:x12"), functions.XS_QName("eba_ZZ:x13"));
        }

        public bool ebav4011a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_GA:AL"), functions.XS_QName("eba_GA:AT"), functions.XS_QName("eba_GA:BE"), functions.XS_QName("eba_GA:BG"), functions.XS_QName("eba_GA:CY"), functions.XS_QName("eba_GA:CZ"), functions.XS_QName("eba_GA:DK"), functions.XS_QName("eba_GA:EE"), functions.XS_QName("eba_GA:FI"), functions.XS_QName("eba_GA:FR"), functions.XS_QName("eba_GA:DE"), functions.XS_QName("eba_GA:GR"), functions.XS_QName("eba_GA:HU"), functions.XS_QName("eba_GA:IE"), functions.XS_QName("eba_GA:IT"), functions.XS_QName("eba_GA:JP"), functions.XS_QName("eba_GA:LV"), functions.XS_QName("eba_GA:LT"), functions.XS_QName("eba_GA:LU"), functions.XS_QName("eba_GA:MK"), functions.XS_QName("eba_GA:MT"), functions.XS_QName("eba_GA:NL"), functions.XS_QName("eba_GA:NO"), functions.XS_QName("eba_GA:x28"), functions.XS_QName("eba_GA:PL"), functions.XS_QName("eba_GA:PT"), functions.XS_QName("eba_GA:RO"), functions.XS_QName("eba_GA:RU"), functions.XS_QName("eba_GA:RS"), functions.XS_QName("eba_GA:SK"), functions.XS_QName("eba_GA:SI"), functions.XS_QName("eba_GA:ES"), functions.XS_QName("eba_GA:SE"), functions.XS_QName("eba_GA:CH"), functions.XS_QName("eba_GA:TR"), functions.XS_QName("eba_GA:UA"), functions.XS_QName("eba_GA:GB"), functions.XS_QName("eba_GA:US"), functions.XS_QName("eba_GA:AF"), functions.XS_QName("eba_GA:AX"), functions.XS_QName("eba_GA:DZ"), functions.XS_QName("eba_GA:AS"), functions.XS_QName("eba_GA:AD"), functions.XS_QName("eba_GA:AO"), functions.XS_QName("eba_GA:AI"), functions.XS_QName("eba_GA:AQ"), functions.XS_QName("eba_GA:AG"), functions.XS_QName("eba_GA:AR"), functions.XS_QName("eba_GA:AM"), functions.XS_QName("eba_GA:AW"), functions.XS_QName("eba_GA:AU"), functions.XS_QName("eba_GA:AZ"), functions.XS_QName("eba_GA:BS"), functions.XS_QName("eba_GA:BH"), functions.XS_QName("eba_GA:BD"), functions.XS_QName("eba_GA:BB"), functions.XS_QName("eba_GA:BY"), functions.XS_QName("eba_GA:BZ"), functions.XS_QName("eba_GA:BJ"), functions.XS_QName("eba_GA:BM"), functions.XS_QName("eba_GA:BT"), functions.XS_QName("eba_GA:BO"), functions.XS_QName("eba_GA:BQ"), functions.XS_QName("eba_GA:BA"), functions.XS_QName("eba_GA:BW"), functions.XS_QName("eba_GA:BV"), functions.XS_QName("eba_GA:BR"), functions.XS_QName("eba_GA:IO"), functions.XS_QName("eba_GA:BN"), functions.XS_QName("eba_GA:BF"), functions.XS_QName("eba_GA:BI"), functions.XS_QName("eba_GA:KH"), functions.XS_QName("eba_GA:CM"), functions.XS_QName("eba_GA:CA"), functions.XS_QName("eba_GA:CV"), functions.XS_QName("eba_GA:KY"), functions.XS_QName("eba_GA:CF"), functions.XS_QName("eba_GA:TD"), functions.XS_QName("eba_GA:CL"), functions.XS_QName("eba_GA:CN"), functions.XS_QName("eba_GA:CX"), functions.XS_QName("eba_GA:CC"), functions.XS_QName("eba_GA:CO"), functions.XS_QName("eba_GA:KM"), functions.XS_QName("eba_GA:CG"), functions.XS_QName("eba_GA:CD"), functions.XS_QName("eba_GA:CK"), functions.XS_QName("eba_GA:CR"), functions.XS_QName("eba_GA:CI"), functions.XS_QName("eba_GA:HR"), functions.XS_QName("eba_GA:CU"), functions.XS_QName("eba_GA:CW"), functions.XS_QName("eba_GA:DJ"), functions.XS_QName("eba_GA:DM"), functions.XS_QName("eba_GA:DO"), functions.XS_QName("eba_GA:EC"), functions.XS_QName("eba_GA:EG"), functions.XS_QName("eba_GA:SV"), functions.XS_QName("eba_GA:GQ"), functions.XS_QName("eba_GA:ER"), functions.XS_QName("eba_GA:ET"), functions.XS_QName("eba_GA:FK"), functions.XS_QName("eba_GA:FO"), functions.XS_QName("eba_GA:FJ"), functions.XS_QName("eba_GA:GF"), functions.XS_QName("eba_GA:PF"), functions.XS_QName("eba_GA:TF"), functions.XS_QName("eba_GA:GA"), functions.XS_QName("eba_GA:GM"), functions.XS_QName("eba_GA:GE"), functions.XS_QName("eba_GA:GH"), functions.XS_QName("eba_GA:GI"), functions.XS_QName("eba_GA:GL"), functions.XS_QName("eba_GA:GD"), functions.XS_QName("eba_GA:GP"), functions.XS_QName("eba_GA:GU"), functions.XS_QName("eba_GA:GT"), functions.XS_QName("eba_GA:GG"), functions.XS_QName("eba_GA:GN"), functions.XS_QName("eba_GA:GW"), functions.XS_QName("eba_GA:GY"), functions.XS_QName("eba_GA:HT"), functions.XS_QName("eba_GA:HM"), functions.XS_QName("eba_GA:VA"), functions.XS_QName("eba_GA:HN"), functions.XS_QName("eba_GA:HK"), functions.XS_QName("eba_GA:IS"), functions.XS_QName("eba_GA:IN"), functions.XS_QName("eba_GA:ID"), functions.XS_QName("eba_GA:IR"), functions.XS_QName("eba_GA:IQ"), functions.XS_QName("eba_GA:IM"), functions.XS_QName("eba_GA:IL"), functions.XS_QName("eba_GA:JM"), functions.XS_QName("eba_GA:JE"), functions.XS_QName("eba_GA:JO"), functions.XS_QName("eba_GA:KZ"), functions.XS_QName("eba_GA:KE"), functions.XS_QName("eba_GA:KI"), functions.XS_QName("eba_GA:KP"), functions.XS_QName("eba_GA:KR"), functions.XS_QName("eba_GA:KW"), functions.XS_QName("eba_GA:KG"), functions.XS_QName("eba_GA:LA"), functions.XS_QName("eba_GA:LB"), functions.XS_QName("eba_GA:LS"), functions.XS_QName("eba_GA:LR"), functions.XS_QName("eba_GA:LY"), functions.XS_QName("eba_GA:LI"), functions.XS_QName("eba_GA:MO"), functions.XS_QName("eba_GA:MG"), functions.XS_QName("eba_GA:MW"), functions.XS_QName("eba_GA:MY"), functions.XS_QName("eba_GA:MV"), functions.XS_QName("eba_GA:ML"), functions.XS_QName("eba_GA:MH"), functions.XS_QName("eba_GA:MQ"), functions.XS_QName("eba_GA:MR"), functions.XS_QName("eba_GA:MU"), functions.XS_QName("eba_GA:YT"), functions.XS_QName("eba_GA:MX"), functions.XS_QName("eba_GA:FM"), functions.XS_QName("eba_GA:MD"), functions.XS_QName("eba_GA:MC"), functions.XS_QName("eba_GA:MN"), functions.XS_QName("eba_GA:ME"), functions.XS_QName("eba_GA:MS"), functions.XS_QName("eba_GA:MA"), functions.XS_QName("eba_GA:MZ"), functions.XS_QName("eba_GA:MM"), functions.XS_QName("eba_GA:NA"), functions.XS_QName("eba_GA:NR"), functions.XS_QName("eba_GA:NP"), functions.XS_QName("eba_GA:NC"), functions.XS_QName("eba_GA:NZ"), functions.XS_QName("eba_GA:NI"), functions.XS_QName("eba_GA:NE"), functions.XS_QName("eba_GA:NG"), functions.XS_QName("eba_GA:NU"), functions.XS_QName("eba_GA:NF"), functions.XS_QName("eba_GA:MP"), functions.XS_QName("eba_GA:OM"), functions.XS_QName("eba_GA:PK"), functions.XS_QName("eba_GA:PW"), functions.XS_QName("eba_GA:PS"), functions.XS_QName("eba_GA:PA"), functions.XS_QName("eba_GA:PG"), functions.XS_QName("eba_GA:PY"), functions.XS_QName("eba_GA:PE"), functions.XS_QName("eba_GA:PH"), functions.XS_QName("eba_GA:PN"), functions.XS_QName("eba_GA:PR"), functions.XS_QName("eba_GA:QA"), functions.XS_QName("eba_GA:RE"), functions.XS_QName("eba_GA:RW"), functions.XS_QName("eba_GA:BL"), functions.XS_QName("eba_GA:SH"), functions.XS_QName("eba_GA:KN"), functions.XS_QName("eba_GA:LC"), functions.XS_QName("eba_GA:MF"), functions.XS_QName("eba_GA:PM"), functions.XS_QName("eba_GA:VC"), functions.XS_QName("eba_GA:WS"), functions.XS_QName("eba_GA:SM"), functions.XS_QName("eba_GA:ST"), functions.XS_QName("eba_GA:SA"), functions.XS_QName("eba_GA:SN"), functions.XS_QName("eba_GA:SC"), functions.XS_QName("eba_GA:SL"), functions.XS_QName("eba_GA:SG"), functions.XS_QName("eba_GA:SX"), functions.XS_QName("eba_GA:SB"), functions.XS_QName("eba_GA:SO"), functions.XS_QName("eba_GA:ZA"), functions.XS_QName("eba_GA:GS"), functions.XS_QName("eba_GA:SS"), functions.XS_QName("eba_GA:LK"), functions.XS_QName("eba_GA:SD"), functions.XS_QName("eba_GA:SR"), functions.XS_QName("eba_GA:SJ"), functions.XS_QName("eba_GA:SZ"), functions.XS_QName("eba_GA:SY"), functions.XS_QName("eba_GA:TW"), functions.XS_QName("eba_GA:TJ"), functions.XS_QName("eba_GA:TZ"), functions.XS_QName("eba_GA:TH"), functions.XS_QName("eba_GA:TL"), functions.XS_QName("eba_GA:TG"), functions.XS_QName("eba_GA:TK"), functions.XS_QName("eba_GA:TO"), functions.XS_QName("eba_GA:TT"), functions.XS_QName("eba_GA:TN"), functions.XS_QName("eba_GA:TM"), functions.XS_QName("eba_GA:TC"), functions.XS_QName("eba_GA:TV"), functions.XS_QName("eba_GA:UG"), functions.XS_QName("eba_GA:AE"), functions.XS_QName("eba_GA:UM"), functions.XS_QName("eba_GA:UY"), functions.XS_QName("eba_GA:UZ"), functions.XS_QName("eba_GA:VU"), functions.XS_QName("eba_GA:VE"), functions.XS_QName("eba_GA:VN"), functions.XS_QName("eba_GA:VG"), functions.XS_QName("eba_GA:VI"), functions.XS_QName("eba_GA:WF"), functions.XS_QName("eba_GA:EH"), functions.XS_QName("eba_GA:YE"), functions.XS_QName("eba_GA:ZM"), functions.XS_QName("eba_GA:ZW"), functions.XS_QName("eba_GA:_1A"), functions.XS_QName("eba_GA:_1B"), functions.XS_QName("eba_GA:_1C"), functions.XS_QName("eba_GA:_1D"), functions.XS_QName("eba_GA:_1E"), functions.XS_QName("eba_GA:_1F"), functions.XS_QName("eba_GA:_1G"), functions.XS_QName("eba_GA:_1H"), functions.XS_QName("eba_GA:_1J"), functions.XS_QName("eba_GA:_1K"), functions.XS_QName("eba_GA:_1L"), functions.XS_QName("eba_GA:_1M"), functions.XS_QName("eba_GA:_1N"), functions.XS_QName("eba_GA:_1O"), functions.XS_QName("eba_GA:_1P"), functions.XS_QName("eba_GA:_1Q"), functions.XS_QName("eba_GA:_1R"), functions.XS_QName("eba_GA:_1S"), functions.XS_QName("eba_GA:_1T"), functions.XS_QName("eba_GA:_1Z"), functions.XS_QName("eba_GA:_4A"), functions.XS_QName("eba_GA:_4B"), functions.XS_QName("eba_GA:_4C"), functions.XS_QName("eba_GA:_4D"), functions.XS_QName("eba_GA:_4E"), functions.XS_QName("eba_GA:_4F"), functions.XS_QName("eba_GA:_4G"), functions.XS_QName("eba_GA:_4H"), functions.XS_QName("eba_GA:_4I"), functions.XS_QName("eba_GA:_4V"), functions.XS_QName("eba_GA:_4J"), functions.XS_QName("eba_GA:_4K"), functions.XS_QName("eba_GA:_4L"), functions.XS_QName("eba_GA:_4M"), functions.XS_QName("eba_GA:_4N"), functions.XS_QName("eba_GA:_4O"), functions.XS_QName("eba_GA:_4P"), functions.XS_QName("eba_GA:_4Q"), functions.XS_QName("eba_GA:_4R"), functions.XS_QName("eba_GA:_4S"), functions.XS_QName("eba_GA:_4T"), functions.XS_QName("eba_GA:_4W"), functions.XS_QName("eba_GA:_4X"), functions.XS_QName("eba_GA:_4Y"), functions.XS_QName("eba_GA:_4Z"), functions.XS_QName("eba_GA:_5A"), functions.XS_QName("eba_GA:_5B"), functions.XS_QName("eba_GA:_5C"), functions.XS_QName("eba_GA:_5D"), functions.XS_QName("eba_GA:_5E"), functions.XS_QName("eba_GA:_5F"), functions.XS_QName("eba_GA:_5G"), functions.XS_QName("eba_GA:_5H"), functions.XS_QName("eba_GA:_5I"), functions.XS_QName("eba_GA:_5J"), functions.XS_QName("eba_GA:_5K"), functions.XS_QName("eba_GA:_5L"), functions.XS_QName("eba_GA:_5M"), functions.XS_QName("eba_GA:_5N"), functions.XS_QName("eba_GA:_5O"), functions.XS_QName("eba_GA:_5P"), functions.XS_QName("eba_GA:_5Q"), functions.XS_QName("eba_GA:_5R"), functions.XS_QName("eba_GA:_5S"), functions.XS_QName("eba_GA:_5T"), functions.XS_QName("eba_GA:_5U"), functions.XS_QName("eba_GA:_5V"), functions.XS_QName("eba_GA:_5W"), functions.XS_QName("eba_GA:_5X"), functions.XS_QName("eba_GA:_5Y"), functions.XS_QName("eba_GA:_5Z"), functions.XS_QName("eba_GA:_6A"), functions.XS_QName("eba_GA:_6B"), functions.XS_QName("eba_GA:_6C"), functions.XS_QName("eba_GA:_6D"), functions.XS_QName("eba_GA:_6E"), functions.XS_QName("eba_GA:_6F"), functions.XS_QName("eba_GA:_6G"), functions.XS_QName("eba_GA:_6H"), functions.XS_QName("eba_GA:_6I"), functions.XS_QName("eba_GA:_6J"), functions.XS_QName("eba_GA:_6K"), functions.XS_QName("eba_GA:_6L"), functions.XS_QName("eba_GA:_6M"), functions.XS_QName("eba_GA:_6N"), functions.XS_QName("eba_GA:_6O"), functions.XS_QName("eba_GA:_6P"), functions.XS_QName("eba_GA:_6Q"), functions.XS_QName("eba_GA:_6R"), functions.XS_QName("eba_GA:_6S"), functions.XS_QName("eba_GA:_6T"), functions.XS_QName("eba_GA:_6U"), functions.XS_QName("eba_GA:_6Z"), functions.XS_QName("eba_GA:_7Z"), functions.XS_QName("eba_GA:_8A"), functions.XS_QName("eba_GA:_9B"));
        }

        public bool ebav4014a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_UE:x1"), functions.XS_QName("eba_UE:x2"), functions.XS_QName("eba_UE:x3"), functions.XS_QName("eba_UE:x4"), functions.XS_QName("eba_UE:x5"), functions.XS_QName("eba_UE:x6"), functions.XS_QName("eba_UE:x8"), functions.XS_QName("eba_UE:x9"), functions.XS_QName("eba_UE:x10"), functions.XS_QName("eba_UE:x12"));
        }

        public bool ebav4015a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_ZZ:x3"), functions.XS_QName("eba_ZZ:x4"), functions.XS_QName("eba_ZZ:x5"), functions.XS_QName("eba_ZZ:x6"));
        }

        public bool ebav4018a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_RS:x1"), functions.XS_QName("eba_RS:x2"), functions.XS_QName("eba_RS:x5"), functions.XS_QName("eba_RS:x6"));
        }

        public bool ebav4020a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_UE:x14"), functions.XS_QName("eba_UE:x15"));
        }

        public bool ebav4021a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_PL:x11"), functions.XS_QName("eba_PL:x51"), functions.XS_QName("eba_PL:x72"), functions.XS_QName("eba_PL:x78"));
        }

        public bool ebav4022a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_TP:x1"), functions.XS_QName("eba_TP:x2"));
        }

        public bool ebav4024a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_RT:x10"), functions.XS_QName("eba_RT:x11"));
        }

        public bool ebav0554m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0555m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0556m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav3757s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0558m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 12.5));
        }

        public bool ebav0560m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 12.5));
        }

        public bool ebav0563m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Greater(p_a, 0) | functions.N_Greater(p_b, 0) | functions.N_Greater(p_c, 0) ? functions.N_Greater(p_d, 0) : true;
        }

        public bool ebav1141m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_aa = parameters.FirstOrDefault(i => i.Name == "aa").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_bb = parameters.FirstOrDefault(i => i.Name == "bb").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_cc = parameters.FirstOrDefault(i => i.Name == "cc").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_dd = parameters.FirstOrDefault(i => i.Name == "dd").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ee = parameters.FirstOrDefault(i => i.Name == "ee").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_k = parameters.FirstOrDefault(i => i.Name == "k").DoubleValue;
            var p_l = parameters.FirstOrDefault(i => i.Name == "l").DoubleValue;
            var p_m = parameters.FirstOrDefault(i => i.Name == "m").DoubleValue;
            var p_n = parameters.FirstOrDefault(i => i.Name == "n").DoubleValue;
            var p_o = parameters.FirstOrDefault(i => i.Name == "o").DoubleValue;
            var p_p = parameters.FirstOrDefault(i => i.Name == "p").DoubleValue;
            var p_q = parameters.FirstOrDefault(i => i.Name == "q").DoubleValue;
            var p_r = parameters.FirstOrDefault(i => i.Name == "r").DoubleValue;
            var p_s = parameters.FirstOrDefault(i => i.Name == "s").DoubleValue;
            var p_t = parameters.FirstOrDefault(i => i.Name == "t").DoubleValue;
            var p_u = parameters.FirstOrDefault(i => i.Name == "u").DoubleValue;
            var p_v = parameters.FirstOrDefault(i => i.Name == "v").DoubleValue;
            var p_w = parameters.FirstOrDefault(i => i.Name == "w").DoubleValue;
            var p_x = parameters.FirstOrDefault(i => i.Name == "x").DoubleValue;
            var p_y = parameters.FirstOrDefault(i => i.Name == "y").DoubleValue;
            var p_z = parameters.FirstOrDefault(i => i.Name == "z").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Greater(p_a, 0) ? functions.N_Greater(p_b, 0) | functions.N_Greater(p_c, 0) | functions.N_Greater(p_d, 0) | functions.N_Greater(p_e, 0) | functions.N_Greater(p_f, 0) | functions.N_Greater(p_g, 0) | functions.N_Greater(p_h, 0) | functions.N_Greater(p_i, 0) | functions.N_Greater(p_j, 0) | functions.N_Greater(p_k, 0) | functions.N_Greater(p_l, 0) | functions.N_Greater(p_m, 0) | functions.N_Greater(p_n, 0) | functions.N_Greater(p_o, 0) | functions.N_Greater(p_p, 0) | functions.N_Greater(p_q, 0) | functions.N_Greater(p_r, 0) | functions.N_Greater(p_s, 0) | functions.N_Greater(p_t, 0) | functions.N_Greater(p_u, 0) | functions.N_Greater(p_v, 0) | functions.N_Greater(p_w, 0) | functions.N_Greater(p_x, 0) | functions.N_Greater(p_y, 0) | functions.N_Greater(p_z, 0) | functions.N_Greater(p_aa, 0) | functions.N_Greater(p_bb, 0) | functions.N_Greater(p_cc, 0) | functions.N_Greater(p_dd, 0) | functions.N_Greater(p_ee, 0) : true;
        }

        public bool ebav3758s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3759s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0562m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(p_b, 12.5));
        }

        public bool ebav1142m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Greater(p_a, 0) ? functions.N_Greater(p_b, 0) | functions.N_Greater(p_c, 0) | functions.N_Greater(p_d, 0) : true;
        }

        public bool ebav1143m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.N_Unary_Minus(p_c), functions.N_Unary_Minus(p_d), functions.N_Unary_Minus(p_e)));
        }

        public bool ebav1144m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav2055s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3760s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0568m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav3762s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0581m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0582m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0583m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0589m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0590m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0592m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0593m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0594m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0596m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0597m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0598m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav2056s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3768s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3769s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3770s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3771s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3772s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3773s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4031m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4032m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4033m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4034m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4035m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4036m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4037m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4038m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4039m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4040m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4041m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4042m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4043m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4044m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4045m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav4046m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e)), p_f));
        }

        public bool ebav0600m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0602m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0604m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0605m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav0606m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav0607m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav0608m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav0609m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav0610m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0611m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d, p_e, p_f));
        }

        public bool ebav0612m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0613m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0614m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0615m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0616m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0617m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav2057s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3774s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3775s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3776s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3777s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4095m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4096m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4097m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav4098m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Divide(functions.sum(functions.N_Multiply(p_b, p_c), functions.N_Multiply(p_d, p_e), functions.N_Multiply(p_f, p_g)), p_h));
        }

        public bool ebav0629m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0635m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.N_Multiply(functions.max(p_b, p_c), 0.08));
        }

        public bool ebav0636m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0637m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(functions.max(p_b, p_c), functions.max(p_d, p_e), functions.max(p_f, p_g), functions.max(p_h, p_i, p_j)));
        }

        public bool ebav1906h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav1907h(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav3792s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3793s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0639m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.N_Multiply(p_a, 12.5), p_b);
        }

        public bool ebav0641m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c, p_d));
        }

        public bool ebav0642m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(functions.max(p_b, p_c), functions.max(p_d, p_e)));
        }

        public bool ebav3794s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3795s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3796s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3797s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0640m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0643m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0644m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0645m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0668m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav3810s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3811s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0670m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0672m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0673m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0674m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0675m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.sum(p_c)));
        }

        public bool ebav0677m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0678m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(functions.sum(p_a), functions.sum(p_b));
        }

        public bool ebav0683m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0684m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav3812s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0700m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0701m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0702m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0703m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0704m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0705m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0706m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0707m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav3813s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0709m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.sum(p_b));
        }

        public bool ebav0710m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, functions.sum(p_b));
        }

        public bool ebav0712m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0713m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0714m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0715m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0716m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0717m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, p_c));
        }

        public bool ebav0718m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav0719m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, p_b);
        }

        public bool ebav3814s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav0721m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, p_b);
        }

        public bool ebav0722m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b));
        }

        public bool ebav0726m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, functions.sum(p_b, functions.sum(p_c), functions.sum(p_d)));
        }

        public bool ebav0727m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a != 0 & p_b != 0 & p_c != 0 ? functions.N_Equals(p_d, functions.N_Divide(functions.sum(p_e), 3)) : true;
        }

        public bool ebav0728m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_Equals(p_a, 0) & functions.N_Equals(p_b, 0) ? functions.N_Equals(p_c, p_d) : true;
        }

        public bool ebav0730m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, p_b);
        }

        public bool ebav3815s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav1156m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_i = parameters.FirstOrDefault(i => i.Name == "i").DoubleValue;
            var p_j = parameters.FirstOrDefault(i => i.Name == "j").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, functions.max(p_b, p_c, p_d, p_e, p_f, p_g, p_h, p_i, p_j));
        }

        public bool ebav1157m(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_b = parameters.FirstOrDefault(i => i.Name == "b").DoubleValue;
            var p_c = parameters.FirstOrDefault(i => i.Name == "c").DoubleValue;
            var p_d = parameters.FirstOrDefault(i => i.Name == "d").DoubleValue;
            var p_e = parameters.FirstOrDefault(i => i.Name == "e").DoubleValue;
            var p_f = parameters.FirstOrDefault(i => i.Name == "f").DoubleValue;
            var p_g = parameters.FirstOrDefault(i => i.Name == "g").DoubleValue;
            var p_h = parameters.FirstOrDefault(i => i.Name == "h").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, functions.max(p_b, p_c, p_d, p_e, p_f, p_g, p_h));
        }

        public bool ebav3761s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3687s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3706s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3716s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3717s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3718s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3719s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_LessEqual(p_a, 0);
        }

        public bool ebav3720s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3726s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3727s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav3728s(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").DoubleValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return functions.N_GreaterEqual(p_a, 0);
        }

        public bool ebav4002c(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_ReportingLevel == "con" ? p_a == functions.XS_QName("eba_SC:x7") : true;
        }

        public bool ebav4003c(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_ReportingLevel == "ind" ? p_a == functions.XS_QName("eba_SC:x6") : true;
        }

        public bool ebav4008a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_ZZ:x16"), functions.XS_QName("eba_ZZ:x17"), functions.XS_QName("eba_ZZ:x18"));
        }

        public bool ebav4009a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_ZZ:x25"), functions.XS_QName("eba_ZZ:x26"));
        }

        public bool ebav4016a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_ZZ:x21"), functions.XS_QName("eba_ZZ:x22"), functions.XS_QName("eba_ZZ:x23"), functions.XS_QName("eba_ZZ:x24"));
        }

        public bool ebav4029a(List<ValidationParameter> parameters)
        {
            var p_a = parameters.FirstOrDefault(i => i.Name == "a").StringValue;
            var p_ReportingLevel = parameters.FirstOrDefault(i => i.Name == "ReportingLevel").StringValue;
            return p_a.In(functions.XS_QName("eba_AP:x38"), functions.XS_QName("eba_AP:x76"));
        }


    }
}
