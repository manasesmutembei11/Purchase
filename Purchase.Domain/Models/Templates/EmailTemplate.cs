using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Templates
{
    public enum SmsTemplateType
    {
        [Description("SUBMISSION OF THE INVESTIGATOR’S REPORT")]
        SubmissionOfInvestigatorReport = 1,


    }

    public enum EmailTemplateType
    {

        [Description("EMAIL CONFIRMATION EMAIL Template")]
        EmailConfirmation = 1,
        [Description("PASSWORD RESET EMAIL Template")]
        ResetPassword = 2,

        [Description("INVESTIGATION : APPOINTMENT OF EXTERNAL EMAIL Template")]
        InvestigatorAppointExternal = 10,
        [Description("INVESTIGATION : APPOINTMENT OF INTERNAL EMAIL Template")]
        InvestigatorAppointInternal = 12,

        [Description("INVESTIGATION : APPOINTMENT OF EXTERNAL TO INSURED EMAIL Template")]
        InvestigatorAppointExtenalToInsured = 13,

        [Description("INVESTIGATION : SUBMISSION OF PRELIMINARY REPORT EMAIL Template")]
        InvestigatorSubmissionOfPreliminaryReport = 14,

        [Description("INVESTIGATION : SUBMISSION OF  FINAL REPORT EMAIL Template")]
        InvestigatorSubmissionOfFinalReport = 15,

        [Description("INVESTIGATION : SUBMISSION OF  FORENSIC REVIEW EMAIL Template")]
        InvestigatorSubmissionOfForensicReview = 16,


        [Description("TECHNICAL ASSESSMENT : APPOINTMENT OF EXTERNAL EMAIL Template")]
        TechnicalAsessmentAppointExternal = 30,

        [Description("TECHNICAL ASSESSMENT : APPOINTMENT OF EXTERNAL TO INSURED EMAIL Template")]
        TechnicalAsessmentAppointExtenalToInsured = 31,

        [Description("TECHNICAL ASSESSMENT :  SUBMISSION OF PRELIMINARY REPORT EMAIL Template")]
        TechnicalAsessmentSubmissionOfPreliminaryReport = 32,

        [Description("TECHNICAL ASSESSMENT :  SUBMISSION OF  FINAL REPORT EMAIL Template")]
        TechnicalAsessmentSubmissionOfFinalReport = 33,

        [Description("TECHNICAL ASSESSMENT :  RETURN OF  FINAL REPORT EMAIL Template")]
        TechnicalAsessmentReturnOfFinalReport = 34,


        [Description("LOSS ADJUSTMENT : APPOINTMENT OF EXTERNAL EMAIL Template")]
        LossAdjustmentAppointExternal = 50,

        [Description("LOSS ADJUSTMENT : APPOINTMENT OF EXTERNAL TO INSURED EMAIL Template")]
        LossAdjustmentAppointExtenalToInsured = 51,

        [Description("LOSS ADJUSTMENT :  SUBMISSION OF PRELIMINARY REPORT EMAIL Template")]
        TLossAdjustmentSubmissionOfPreliminaryReport = 52,

        [Description("LOSS ADJUSTMENT :  SUBMISSION OF  FINAL REPORT EMAIL Template")]
        LossAdjustmentSubmissionOfFinalReport = 53,

        [Description("LOSS ADJUSTMENT :  RETURN OF  FINAL REPORT EMAIL Template")]
        LossAdjustmentReturnOfFinalReport = 54,


        [Description("LEGAL : APPOINTMENT OF AN ADVOCATE EMAIL Template")]
        LegalAdvocateAppointment = 60,

        [Description("LEGAL : APPOINTMENT OF AN ADVOCATE TO INSURED EMAIL Template")]
        LegalAdvocateAppointmentToInsured = 61,



    }
    public class EmailTemplate : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public EmailTemplateType EmailTemplateType { get; set; }

        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
