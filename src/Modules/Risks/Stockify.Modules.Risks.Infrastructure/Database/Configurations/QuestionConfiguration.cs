using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable(
            TableNames.Questions,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Questions_Weight",
                    sql: "weight > 0");
            });

        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => QuestionId.From(value)
            );

        builder.Property(q => q.Content).HasMaxLength(500);
        
        builder.Property(q => q.Category)
            .HasConversion(
                category => category.Name,
                value => QuestionCategory.FromName(value)
            )
            .HasMaxLength(100);

        builder.HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey(a => a.QuestionId)
            .IsRequired();
        
        builder.HasData(CreateQuestions());
    }
    
    private static List<Question> CreateQuestions()
    {
        var questions = new List<Question>();
        
        // Risk Tolerance Questions
        (string, (string, int)[])[] riskToleranceQuestions =
        [
            (
                "How would you react if your investment portfolio lost 20% of its value in one month?",
                [
                    ("I would sell everything immediately to prevent further losses", 1),
                    ("I would be concerned but would wait to see if it recovers", 3),
                    ("I would see it as an opportunity to invest more", 5)
                ]
            ),
            (
                "What is your primary investment goal?",
                [
                    ("Preserving my capital is my main priority", 1),
                    ("A balance between growth and preservation", 3),
                    ("Maximum growth, even with significant risk", 5)
                ]
            ),
            (
                "When choosing investments, what is most important to you?",
                [
                    ("Minimizing the chance of losing money", 1),
                    ("Keeping a balance between gains and losses", 3),
                    ("Maximizing potential gains", 5)
                ]
            ),
            (
                "How would you feel about investing in a volatile stock market?",
                [
                    ("Very uncomfortable", 1),
                    ("Somewhat comfortable", 3),
                    ("Very comfortable", 5)
                ]
            ),
            (
                "What would you do if one of your investments dropped 25% in value?",
                [
                    ("Sell immediately", 1),
                    ("Hold and wait for recovery", 3),
                    ("Buy more at the lower price", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in riskToleranceQuestions)
        {
            var question = Question.Create(content, QuestionCategory.RiskTolerance);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        // Loss Tolerance Questions
        (string, (string, int)[])[] lossToleranceQuestions =
        [
            (
                "What maximum percentage of your investment are you willing to lose in pursuit of higher returns?",
                [
                    ("No more than 5%", 1),
                    ("Up to 15%", 3),
                    ("Up to 30% or more", 5)
                ]
            ),
            (
                "How long would you be willing to wait for your portfolio to recover from a significant loss?",
                [
                    ("Less than 6 months", 1),
                    ("1-2 years", 3),
                    ("3+ years if necessary", 5)
                ]
            ),
            (
                "How would you feel about a 15% decline in your investment value?",
                [
                    ("Very uncomfortable, I would have trouble sleeping", 1),
                    ("Concerned, but I understand it's part of investing", 3),
                    ("I see it as a normal part of investing", 5)
                ]
            ),
            (
                "What level of temporary loss in your portfolio would cause you to change your investment strategy?",
                [
                    ("10% or less", 1),
                    ("20-30%", 3),
                    ("More than 30%", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in lossToleranceQuestions)
        {
            var question = Question.Create(content, QuestionCategory.LossTolerance);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        // Investment Horizon Questions
        (string, (string, int)[])[] investmentHorizonQuestions =
        [
            (
                "When do you expect to need most of your invested money?",
                [
                    ("Within the next 2 years", 1),
                    ("In 3-5 years", 3),
                    ("Not for at least 7 years", 5)
                ]
            ),
            (
                "What percentage of your investments would you need access to within a year?",
                [
                    ("More than 50%", 1),
                    ("25-50%", 3),
                    ("Less than 25%", 5)
                ]
            ),
            (
                "How long are you planning to keep your money invested?",
                [
                    ("1-3 years", 1),
                    ("4-7 years", 3),
                    ("More than 7 years", 5)
                ]
            ),
            (
                "What is your primary time horizon for achieving your investment goals?",
                [
                    ("Short-term (less than 3 years)", 1),
                    ("Medium-term (3-7 years)", 3),
                    ("Long-term (more than 7 years)", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in investmentHorizonQuestions)
        {
            var question = Question.Create(content, QuestionCategory.InvestmentHorizon);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        // Income Stability Questions
        (string, (string, int)[])[] incomeStabilityQuestions =
        [
            (
                "How stable is your current income source?",
                [
                    ("Variable or uncertain", 1),
                    ("Stable but with some variations", 3),
                    ("Very stable", 5)
                ]
            ),
            (
                "How many months of expenses could you cover with your emergency savings?",
                [
                    ("Less than 3 months", 1),
                    ("3-6 months", 3),
                    ("More than 6 months", 5)
                ]
            ),
            (
                "How would you describe your job security?",
                [
                    ("Uncertain or changing", 1),
                    ("Generally stable", 3),
                    ("Very secure", 5)
                ]
            ),
            (
                "What percentage of your monthly income goes to fixed expenses?",
                [
                    ("More than 70%", 1),
                    ("40-70%", 3),
                    ("Less than 40%", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in incomeStabilityQuestions)
        {
            var question = Question.Create(content, QuestionCategory.IncomeStability);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        // Financial Knowledge Questions
        (string, (string, int)[])[] financialKnowledgeQuestions =
        [
            (
                "How would you rate your understanding of financial markets and investment products?",
                [
                    ("Limited understanding", 1),
                    ("Good understanding of basics", 3),
                    ("Advanced understanding", 5)
                ]
            ),
            (
                "How familiar are you with the concept of risk diversification?",
                [
                    ("Not familiar", 1),
                    ("Somewhat familiar", 3),
                    ("Very familiar", 5)
                ]
            ),
            (
                "How well do you understand the relationship between risk and return?",
                [
                    ("Limited understanding", 1),
                    ("Basic understanding", 3),
                    ("Comprehensive understanding", 5)
                ]
            ),
            (
                "How would you rate your knowledge of different investment vehicles (stocks, bonds, ETFs)?",
                [
                    ("Limited knowledge", 1),
                    ("Moderate knowledge", 3),
                    ("Extensive knowledge", 5)
                ]
            ),
            (
                "How comfortable are you with financial terminology and concepts?",
                [
                    ("Not comfortable", 1),
                    ("Moderately comfortable", 3),
                    ("Very comfortable", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in financialKnowledgeQuestions)
        {
            var question = Question.Create(content, QuestionCategory.FinancialKnowledge);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        // Investment Experience Questions
        (string, (string, int)[])[] investmentExperienceQuestions =
        [
            (
                "How long have you been investing in financial markets?",
                [
                    ("No experience", 1),
                    ("1-3 years", 3),
                    ("More than 3 years", 5)
                ]
            ),
            (
                "What types of investments have you made in the past?",
                [
                    ("Mainly savings accounts and CDs", 1),
                    ("Mutual funds and some stocks", 3),
                    ("Stocks, bonds, and other complex instruments", 5)
                ]
            ),
            (
                "How often do you review and rebalance your investment portfolio?",
                [
                    ("Rarely or never", 1),
                    ("Annually", 3),
                    ("Quarterly or more frequently", 5)
                ]
            ),
            (
                "What is the most complex investment product you have used?",
                [
                    ("Savings accounts/CDs", 1),
                    ("Mutual funds/ETFs", 3),
                    ("Options/Futures/Complex derivatives", 5)
                ]
            ),
            (
                "How would you describe your investment decision-making process?",
                [
                    ("I rely completely on others' advice", 1),
                    ("I do some research but also seek advice", 3),
                    ("I do thorough research and make my own decisions", 5)
                ]
            )
        ];

        foreach ((string content, (string, int)[] answers) in investmentExperienceQuestions)
        {
            var question = Question.Create(content, QuestionCategory.InvestmentExperience);
            foreach ((string answerContent, int points) in answers)
            {
                question.AddAnswer(answerContent, points);
            }
            questions.Add(question);
        }

        return questions;
    }
}
